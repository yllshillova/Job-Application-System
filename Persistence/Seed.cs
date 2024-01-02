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




            var jobSeekers = new List<JobSeeker>
            {
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
                }

            };

            await context.JobSeekers.AddRangeAsync(jobSeekers);
            await context.SaveChangesAsync();

            var entrepreneurs = new List<Entrepreneur>
            {
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
            await context.Entrepreneurs.AddRangeAsync(entrepreneurs);
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
                EntrepreneurId = entrepreneurs[0].Id
            },
            new Company
            {
                Name = "Marketing Pros Ltd.",
                Industry = "Marketing",
                Location = "New York",
                Description = "Leading marketing agency",
                Email = "info@marketingpros.com",
                EntrepreneurId = entrepreneurs[0].Id
            },
            new Company
            {
                Name = "Health Innovations Corp.",
                Industry = "Healthcare",
                Location = "Boston",
                Description = "Revolutionizing healthcare solutions",
                Email = "info@healthinnovations.com",
                EntrepreneurId = entrepreneurs[0].Id
            }
        };

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();


            var recruiters = new List<Recruiter>
            {
                 new Recruiter
                {
                    Name = "user1",
                    LastName = "userlastname1",
                    Email = "test@test.com",
                    Password = "test1",
                    Headline = "headline1",
                    Summary = "summary2",
                    Location = "location1",
                    ContactNumber = "contactnumber",
                    CompanyId = companies[0].Id,
                    Educations = new List<Education>
                    {
                        new Education
                        {
                            Institution = "Startup University",
                            Degree = "Entrepreneurship",
                            GraduationYear = "2021"
                        }
                    },
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            SkillName = "Product Development",
                            SkillDescription = "Creating innovative products."
                        },
                        new Skill
                        {
                            SkillName = "Strategy",
                            SkillDescription = "Developing business strategies."
                        }
                    },
                    Experiences = new List<Experience>
                    {
                        new Experience
                        {
                            Position = "Tech Recruiter",
                            CompanyName = "Tech Recruiting Co.",
                            Location = "New York City",
                            StartDate = DateTime.UtcNow.AddYears(-3),
                            Description = "Recruitment for top tech companies."
                        }
                    }
                }
            };

            await context.Recruiters.AddRangeAsync(recruiters);
            await context.SaveChangesAsync();




            var jobPosts = new List<JobPost>
            {
                new JobPost
                {
                    Title = "Software Developer",
                    Description = "Looking for a skilled software developer.",
                    Requirements = "Experience in C# and ASP.NET Core.",
                    DatePosted = DateTime.UtcNow,
                    RecruiterId = recruiters[0].Id
                },

            };
            await context.JobPosts.AddRangeAsync(jobPosts);
            await context.SaveChangesAsync();

        }
    }
}
