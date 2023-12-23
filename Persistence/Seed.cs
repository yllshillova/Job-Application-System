using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>();
            var entrepreneurs = new List<Entrepreneur>();
            var jobSeekers = new List<JobSeeker>();
            var recruiters = new List<Recruiter>();
            var companies = new List<Company>();
            var skills = new List<Skill>();
            var educations = new List<Education>();
            var experiences = new List<Experience>();

            // Create 10 instances for each entity
            for (int i = 1; i <= 10; i++)
            {
                // Create User
                var user = new User
                {
                    Name = $"User{i}",
                    LastName = $"Last{i}",
                    Password = $"Password{i}",
                    Email = $"user{i}@example.com",
                    Headline = $"Headline{i}",
                    Summary = $"Summary{i}",
                    Location = $"Location{i}",
                    ContactNumber = $"ContactNumber{i}",
                    Skills = new List<Skill>(),
                    Educations = new List<Education>(),
                    Experiences = new List<Experience>(),
                };

                // Create Skill
                var skill = new Skill
                {
                    SkillName = $"Skill{i}",
                    SkillDescription = $"SkillDescription{i}",
                    UserId = user.Id,
                    User = user,
                };

                user.Skills.Add(skill);
                skills.Add(skill);

                // Create Education
                var education = new Education
                {
                    Institution = $"Institution{i}",
                    Degree = $"Degree{i}",
                    GraduationYear = $"GraduationYear{i}",
                    UserId = user.Id,
                    User = user,
                };

                user.Educations.Add(education);
                educations.Add(education);

                // Create Experience
                var experience = new Experience
                {
                    Position = $"Position{i}",
                    CompanyName = $"CompanyName{i}",
                    Location = $"Location{i}",
                    StartDate = DateTime.Now.AddMonths(-i),
                    EndDate = DateTime.Now.AddMonths(-i + 6),
                    Description = $"Description{i}",
                    UserId = user.Id,
                    User = user,
                };

                user.Experiences.Add(experience);
                experiences.Add(experience);

                users.Add(user);

                // Create Entrepreneur
                var entrepreneur = new Entrepreneur
                {
                    Id = user.Id,
                    Companies = new List<Company>(),
                };

                entrepreneur.Companies.Add(new Company
                {
                    Name = $"Company{i}",
                    Industry = $"Industry{i}",
                    Location = $"CompanyLocation{i}",
                    Description = $"CompanyDescription{i}",
                    Email = $"company{i}@example.com",
                    EntrepreneurId = entrepreneur.Id,
                    Entrepreneur = entrepreneur,
                    Recruiters = new List<Recruiter>(),
                    EmailNotifications = new List<EmailNotification>(),
                });

                entrepreneurs.Add(entrepreneur);

                // Create JobSeeker
                var jobSeeker = new JobSeeker
                {
                    Id = user.Id,
                    Applications = new List<Application>(),
                };

                jobSeekers.Add(jobSeeker);

                // Create Recruiter
                var recruiter = new Recruiter
                {
                    Id = user.Id,
                    CompanyId = entrepreneur.Companies.First().Id,
                    Company = entrepreneur.Companies.First(),
                    JobPosts = new List<JobPost>(),
                };

                recruiter.JobPosts.Add(new JobPost
                {
                    Title = $"JobTitle{i}",
                    Description = $"JobDescription{i}",
                    Requirements = $"JobRequirements{i}",
                    DatePosted = DateTime.Now.AddMonths(-i),
                    RecruiterId = recruiter.Id,
                    Recruiter = recruiter,
                    Applications = new List<Application>(),
                });

                recruiters.Add(recruiter);

               
                // // Create EmailNotification
                // var emailNotification = new EmailNotification
                // {
                //     EmailAddress = $"notification{i}@example.com",
                //     Subject = $"NotificationSubject{i}",
                //     Body = $"NotificationBody{i}",
                //     SentAt = DateTime.Now.ToString(),
                //     CompanyId = entrepreneur.Companies.First().Id,
                //     Company = entrepreneur.Companies.First(),
                // };

                // emailNotifications.Add(emailNotification);
            }


            // Combine all user roles
            var allUsers = entrepreneurs.Cast<User>().Concat(jobSeekers).Concat(recruiters).ToList();
            await context.Users.AddRangeAsync(allUsers);
            await context.Skills.AddRangeAsync(skills);
            await context.Educations.AddRangeAsync(educations);
            await context.Experiences.AddRangeAsync(experiences);
            await context.Entrepreneurs.AddRangeAsync(entrepreneurs);
            await context.JobSeekers.AddRangeAsync(jobSeekers);
            await context.Recruiters.AddRangeAsync(recruiters);
            await context.Companies.AddRangeAsync(companies);
            // await context.JobPosts.AddRangeAsync(jobPosts);
            // await context.EmailNotifications.AddRangeAsync(emailNotifications);

            // Save changes to the database
            await context.SaveChangesAsync();
        }
    }
}