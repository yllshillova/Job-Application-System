using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any()) return;

            var entrepreneurs = new List<Entrepreneur>
        {
            new Entrepreneur
            {
                Name = "Entrepreneur One",
                LastName= "shillova",
                Location = "Silicon Valley, USA",
                Summary = "Visionary leader with a passion for innovation.",
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
                }
            },
            // Add more entrepreneurs if needed
        };

            var jobSeekers = new List<JobSeeker>
        {
            new JobSeeker
            {
                Name = "Jobseeker one",
                Location = "San Francisco, USA",
                Summary = "Seeking opportunities in the tech industry.",
                Educations = new List<Education>
                {
                    new Education
                    {
                        Institution = "Tech Academy",
                        Degree = "Computer Engineering",
                        GraduationYear = "2021"
                    }
                },
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        SkillName = "Java",
                        SkillDescription = "Programming in Java."
                    },
                    new Skill
                    {
                        SkillName = "JavaScript",
                        SkillDescription = "Web development with JavaScript."
                    },
                    new Skill
                    {
                        SkillName = "Data Analysis",
                        SkillDescription = "Analyzing and interpreting data."
                    }
                }
            },
            // Add more job seekers if needed
        };

            var recruiters = new List<Recruiter>
        {
            new Recruiter
            {
                Name = "Recruiter One",
                Location = "New York City, USA",
                Summary = "Experienced recruiter with a focus on tech talent.",
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
            },
            // Add more recruiters if needed
        };

            // Combine all user roles
            var allUsers = entrepreneurs.Cast<User>().Concat(jobSeekers).Concat(recruiters).ToList();

            await context.Users.AddRangeAsync(allUsers);
            await context.SaveChangesAsync();
        }
    }
}