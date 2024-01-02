using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>
            {
                // Regular Users
                new User
                {
                    Name = "John",
                    LastName = "Doe",
                    Password = "password123",
                    Email = "john.doe@example.com",
                    Headline = "Software Developer",
                    Summary = "Experienced software developer with a focus on web development.",
                    Location = "New York",
                    ContactNumber = "+1 (555) 123-4567", // veq dej qetu me bo Userin. 
                    Skills = new List<Skill>
                    {
                        new Skill { SkillName = "C#", SkillDescription = "Programming language" },
                        new Skill { SkillName = "ASP.NET Core", SkillDescription = "Web framework" },
                        new Skill { SkillName = "SQL", SkillDescription = "Database management" }
                    },
                    Educations = new List<Education>
                    {
                        new Education { Institution = "University of New York", Degree = "B.Sc. in Computer Science", GraduationYear = "2015" },
                        new Education { Institution = "Coding Bootcamp", Degree = "Full Stack Development", GraduationYear = "2017" },
                        new Education { Institution = "Online Courses", Degree = "Data Science", GraduationYear = "2019" }
                    },
                    Experiences = new List<Experience>
                    {
                        new Experience { Position = "Software Engineer", CompanyName = "Tech Co.", Location = "New York", StartDate = DateTime.Parse("2020-01-01"), EndDate = null, Description = "Developing scalable web applications." },
                        new Experience { Position = "Junior Developer", CompanyName = "Web Solutions", Location = "New York", StartDate = DateTime.Parse("2018-05-01"), EndDate = DateTime.Parse("2019-12-31"), Description = "Worked on various web projects." },
                        new Experience { Position = "Intern", CompanyName = "Software Innovators", Location = "New York", StartDate = DateTime.Parse("2017-06-01"), EndDate = DateTime.Parse("2017-12-31"), Description = "Assisted in software development tasks." }
                    }
                },
                
                // Add more users of different types (JobSeeker, Recruiter, Entrepreneur) with skills, educations, and experiences.

                // Job Seekers
                new JobSeeker
                {
                    Name = "Alice",
                    LastName = "Johnson",
                    Password = "password456",
                    Email = "alice.johnson@example.com",
                    Headline = "Marketing Specialist",
                    Summary = "Experienced marketing professional with a focus on digital marketing strategies.",
                    Location = "San Francisco",
                    ContactNumber = "+1 (555) 987-6543",
                    Skills = new List<Skill>
                    {
                        new Skill { SkillName = "Digital Marketing", SkillDescription = "Strategic online marketing" },
                        new Skill { SkillName = "SEO", SkillDescription = "Search engine optimization" },
                        new Skill { SkillName = "Social Media Management", SkillDescription = "Managing social media accounts" }
                    },
                    Educations = new List<Education>
                    {
                        new Education { Institution = "University of California", Degree = "B.A. in Marketing", GraduationYear = "2016" },
                        new Education { Institution = "Digital Marketing Certification", Degree = "Online Course", GraduationYear = "2018" },
                        new Education { Institution = "MBA in Marketing", Degree = "University of San Francisco", GraduationYear = "2020" }
                    },
                    Experiences = new List<Experience>
                    {
                        new Experience { Position = "Marketing Manager", CompanyName = "Digital Solutions Inc.", Location = "San Francisco", StartDate = DateTime.Parse("2017-01-01"), EndDate = null, Description = "Developing and implementing digital marketing strategies." },
                        new Experience { Position = "Digital Marketing Specialist", CompanyName = "Tech Innovators", Location = "San Francisco", StartDate = DateTime.Parse("2015-05-01"), EndDate = DateTime.Parse("2016-12-31"), Description = "Managed online advertising campaigns." },
                        new Experience { Position = "Marketing Intern", CompanyName = "Marketing Pro", Location = "San Francisco", StartDate = DateTime.Parse("2014-06-01"), EndDate = DateTime.Parse("2014-12-31"), Description = "Assisted in various marketing projects." }
                    }
                },
                // Entrepreneurs
                new Entrepreneur
                {
                    Name = "Emily",
                    LastName = "Williams",
                    Password = "password012",
                    Email = "emily.williams@example.com",
                    Headline = "Tech Entrepreneur",
                    Summary = "Passionate about creating innovative solutions and building successful tech startups.",
                    Location = "Silicon Valley",
                    ContactNumber = "+1 (555) 234-5678",
                    Skills = new List<Skill>
                    {
                        new Skill { SkillName = "Startup Management", SkillDescription = "Leading and managing startup operations" },
                        new Skill { SkillName = "Product Development", SkillDescription = "Bringing new products to market" },
                        new Skill { SkillName = "Strategic Planning", SkillDescription = "Developing business strategies" }
                    },
                    Educations = new List<Education>
                    {
                        new Education { Institution = "Stanford University", Degree = "B.Sc. in Computer Science", GraduationYear = "2010" },
                        new Education { Institution = "Master's in Business Administration", Degree = "Stanford Graduate School of Business", GraduationYear = "2012" },
                        new Education { Institution = "Entrepreneurship Program", Degree = "Online Course", GraduationYear = "2014" }
                    },
                    Experiences = new List<Experience>
                    {
                        new Experience { Position = "CEO", CompanyName = "Tech Innovations", Location = "Silicon Valley", StartDate = DateTime.Parse("2012-01-01"), EndDate = null, Description = "Founded and led a successful tech startup." },
                        new Experience { Position = "Product Manager", CompanyName = "InnovateTech", Location = "Silicon Valley", StartDate = DateTime.Parse("2010-06-01"), EndDate = DateTime.Parse("2011-12-31"), Description = "Managed product development for a tech company." },
                        new Experience { Position = "Intern", CompanyName = "Startup Hub", Location = "Silicon Valley", StartDate = DateTime.Parse("2009-06-01"), EndDate = DateTime.Parse("2009-12-31"), Description = "Internship in a startup environment." }
                    }
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
            var companies = new List<Company>
        {
            new Company
            {
                Name = "Tech Solutions Inc.",
                Industry = "Technology",
                Location = "Silicon Valley",
                Description = "Innovative technology solutions provider",
                Email = "info@techsolutions.com",
                EntrepreneurId = users[2].Id
            },
            new Company
            {
                Name = "Marketing Pros Ltd.",
                Industry = "Marketing",
                Location = "New York",
                Description = "Leading marketing agency",
                Email = "info@marketingpros.com",
                EntrepreneurId = users[2].Id
            },
            new Company
            {
                Name = "Health Innovations Corp.",
                Industry = "Healthcare",
                Location = "Boston",
                Description = "Revolutionizing healthcare solutions",
                Email = "info@healthinnovations.com",
                EntrepreneurId = users[2].Id
            }
        };

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();




            var jobPosts = new List<JobPost>
            {
                new JobPost
                {
                    Title = "Software Developer",
                    Description = "Looking for a skilled software developer.",
                    Requirements = "Experience in C# and ASP.NET Core.",
                    DatePosted = DateTime.UtcNow,
                    RecruiterId = users[0].Id // Assuming the first user is a Recruiter
                },
                // Add more job posts as needed
            };
            await context.JobPosts.AddRangeAsync(jobPosts);
            await context.SaveChangesAsync();


        }
    }
}
