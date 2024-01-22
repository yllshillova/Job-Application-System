using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            // Seed roles
            if (!await roleManager.RoleExistsAsync("JobSeeker"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("JobSeeker"));
            }

            if (!await roleManager.RoleExistsAsync("Recruiter"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Recruiter"));
            }

            if (!await roleManager.RoleExistsAsync("Entrepreneur"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Entrepreneur"));
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            await context.SaveChangesAsync();

            // Seed Users and Roles
            if (!userManager.Users.Any())
            {
                // Seed Admins
                var admins = new List<AppUser>
                {
                    new AppUser { UserName = "admin1", UserLastName = "admin1", Email = "admin1@test.com" },
                    new AppUser { UserName = "admin2", UserLastName = "admin1", Email = "admin2@test.com" },
                };

                foreach (var admin in admins)
                {
                    await userManager.CreateAsync(admin, "Pa$$w0rd");
                }
                foreach (var admin in admins)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                // Seed JobSeekers
                var jobSeekers = new List<JobSeeker>
                {
                    new JobSeeker { UserName = "JobSeeker1", UserLastName = "seeker", Email = "seeker1@test.com" },
                    new JobSeeker { UserName = "JobSeeker2", UserLastName = "seeker", Email = "seeker2@test.com" },
                    new JobSeeker { UserName = "JobSeeker3", UserLastName = "seeker", Email = "seeker3@test.com" },
                    new JobSeeker { UserName = "JobSeeker4", UserLastName = "seeker", Email = "seeker4@test.com" },
                };

                foreach (var jobSeeker in jobSeekers)
                {
                    await userManager.CreateAsync(jobSeeker, "Pa$$w0rd");

                    // Seed Education, Experience, Skill for each JobSeeker
                    var education = new Education
                    {
                        Institution = "University1",
                        Degree = "Bachelor's in Computer Science",
                        GraduationYear = "2022",
                        UserId = jobSeeker.Id
                    };

                    context.Educations.Add(education);

                    var experience = new Experience
                    {
                        Position = "Software Engineer",
                        CompanyName = "Tech Company",
                        Location = "City1",
                        StartDate = new DateTime(2020, 1, 1),
                        EndDate = new DateTime(2022, 12, 31),
                        Description = "Worked on various software projects.",
                        UserId = jobSeeker.Id
                    };

                    context.Experiences.Add(experience);

                    var skill = new Skill
                    {
                        SkillName = "C#",
                        SkillDescription = "Proficient in C# programming.",
                        UserId = jobSeeker.Id
                    };

                    context.Skills.Add(skill);
                    await context.SaveChangesAsync();

                }
                foreach (var jobSeeker in jobSeekers)
                {
                    await userManager.AddToRoleAsync(jobSeeker, "JobSeeker");
                }


                // Seed Entrepreneurs
                var entrepreneurs = new List<Entrepreneur>
                    {
                        new Entrepreneur { UserName = "Entrepreneur1", UserLastName = "entrepreneur", Email = "Entrepreneur1@test.com" },
                        new Entrepreneur { UserName = "Entrepreneur2", UserLastName = "entrepreneur", Email = "Entrepreneur2@test.com" },
                        new Entrepreneur { UserName = "Entrepreneur3", UserLastName = "entrepreneur", Email = "Entrepreneur3@test.com" },
                        new Entrepreneur { UserName = "Entrepreneur4", UserLastName = "entrepreneur", Email = "Entrepreneur4@test.com" },
                    };

                foreach (var entrepreneur in entrepreneurs)
                {
                    await userManager.CreateAsync(entrepreneur, "Pa$$w0rd");
                }

                await context.SaveChangesAsync();

                // Seed Companies for each Entrepreneur
                foreach (var entrepreneur in entrepreneurs)
                {
                    var company = new Company
                    {
                        Name = "TechCorp",
                        Industry = "Technology",
                        Location = "City1",
                        Description = "A leading tech company...",
                        Email = "info@techcorp.com",
                        EntrepreneurId = entrepreneur.Id
                    };

                    context.Companies.Add(company);
                }

                await context.SaveChangesAsync();

                foreach (var entrepreneur in entrepreneurs)
                {
                    await userManager.AddToRoleAsync(entrepreneur, "Entrepreneur");
                }


                // Seed Recruiters (not associated with companies)
                var recruiters = new List<Recruiter>
                {
                    new Recruiter { UserName = "Recruiter2", UserLastName = "recruiter", Email = "recruiter2@test.com" },
                    new Recruiter { UserName = "Recruiter3", UserLastName = "recruiter", Email = "recruiter3@test.com" },
                    new Recruiter { UserName = "Recruiter4", UserLastName = "recruiter", Email = "recruiter4@test.com" },
                    new Recruiter { UserName = "Recruiter5", UserLastName = "recruiter", Email = "recruiter5@test.com" },
                    new Recruiter { UserName = "Recruiter6", UserLastName = "recruiter", Email = "recruiter6@test.com" },
                };

                await context.SaveChangesAsync();

                // Seed ResumeStorage
                if (!context.ResumeStorages.Any())
                {
                    var resumeStorages = new List<ResumeStorage>
                    {
                        new ResumeStorage
                        {
                            FileName = "John_Doe_Resume.pdf",
                            FileData = Encoding.UTF8.GetBytes("Another sample resume data") // Replace with actual file data
                        },
                        // Add more ResumeStorage entries as needed
                    };

                    context.ResumeStorages.AddRange(resumeStorages);
                    await context.SaveChangesAsync();
                }


                foreach (var entrepreneur in entrepreneurs)
                {
                    var company = context.Companies.Single(c => c.EntrepreneurId == entrepreneur.Id);

                    var recruiter = new Recruiter
                    {
                        UserName = $"Recruiter_{entrepreneur.UserName}",
                        UserLastName = "recruiter",
                        Email = $"recruiter_{entrepreneur.UserName}@test.com",
                        CompanyId = company.Id
                    };

                    await userManager.CreateAsync(recruiter, "Pa$$w0rd");

                    if (recruiter != null)
                    {
                        await userManager.AddToRoleAsync(recruiter, "Recruiter");
                    }

                    // Save changes after creating the Recruiter
                    await context.SaveChangesAsync();

                    // The Recruiter is now available in the database, use its Id for the JobPost
                    var jobPost = new JobPost
                    {
                        Title = "Software Engineer",
                        Description = "Looking for a skilled software engineer...",
                        Requirements = "Bachelor's degree in Computer Science, experience with C#...",
                        DatePosted = DateTime.Now,
                        RecruiterId = recruiter.Id, // Set the RecruiterId using the created Recruiter's Id
                        Applications = new List<ApplicationEntity>() // Initialize the Applications list
                    };

                    context.JobPosts.Add(jobPost);
                    await context.SaveChangesAsync();

                    // Seed Applications for each JobPost
                    foreach (var jobSeeker in jobSeekers)
                    {
                        var application = new ApplicationEntity
                        {
                            Status = "Pending",
                            DateSubmitted = DateTime.Now,
                            ResumeFileId = context.ResumeStorages.First().Id,
                            JobPostId = jobPost.Id,
                            JobSeekerId = jobSeeker.Id,
                           };

                        context.Applications.Add(application);
                    }

                    //Save changes after seeding all Applications for the current JobPost
                    await context.SaveChangesAsync();
                }


                // if (!context.EmailNotifications.Any())
                // {
                //     var jobSeekerIds = new List<Guid>();
                //     foreach (var jobPost in context.JobPosts)
                //     {
                //         var recruiterEmail = jobPost.Recruiter.Email; // Get the email of the recruiter who created the job post
                //         var companyId = jobPost.Recruiter.CompanyId;


                //         // Create a single notification for each company
                //         var emailNotification = new EmailNotification
                //         {
                //             EmailAddress = recruiterEmail,
                //             Subject = "Welcome to our platform!",
                //             Body = $"Thank you for joining...",
                //             SentAt = DateTime.Now.ToString(), // Use the current timestamp or customize as needed
                //             CompanyId = companyId
                //         };

                //         context.EmailNotifications.Add(emailNotification);

                //         // Save changes after seeding EmailNotifications for the current company
                //         await context.SaveChangesAsync();

                //         // Now, you can proceed with seeding Applications for each JobPost
                //         foreach (var jobSeekerId in jobSeekerIds)
                //         {
                //             // Continue with your existing code for seeding Applications
                //             var application = new ApplicationEntity
                //             {
                //                 Status = "Pending",
                //                 DateSubmitted = DateTime.Now,
                //                 ResumeFileId = context.ResumeStorages.First().Id,
                //                 JobPostId = jobPost.Id,
                //                 JobSeekerId = jobSeekerId, // Use the collected JobSeekerId
                //                 EmailNotificationId = emailNotification.Id // Use the Id of the seeded EmailNotification
                //             };

                //             context.Applications.Add(application);
                //         }

                //         // Save changes after seeding all Applications for the current JobPost
                //         await context.SaveChangesAsync();
                //     }
                // }


            }

            await context.SaveChangesAsync();
        }


    }
}
