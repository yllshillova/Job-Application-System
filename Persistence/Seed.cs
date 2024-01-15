using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {


            // Create roles
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

            await context.SaveChangesAsync();

            if (!userManager.Users.Any())
            {
                var jobSeekers = new List<JobSeeker>
            {
                new JobSeeker{UserName = "JobSeeker1", UserLastName = "seeker", Email = "seeker1@test.com"},
                new JobSeeker{UserName = "JobSeeker2", UserLastName = "seeker", Email = "seeker2@test.com"},
                new JobSeeker{UserName = "JobSeeker3", UserLastName = "seeker", Email = "seeker3@test.com"},
                new JobSeeker{UserName = "JobSeeker4", UserLastName = "seeker", Email = "seeker4@test.com"},
            };

                foreach (var jobSeeker in jobSeekers)
                {
                    await userManager.CreateAsync(jobSeeker, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(jobSeeker, "JobSeeker");
                }
                var entrepreneurs = new List<Entrepreneur>
            {
                new Entrepreneur{UserName = "Entrepreneur1", UserLastName = "entrepreneur", Email = "Entrepreneur1@test.com"},
                new Entrepreneur{UserName = "Entrepreneur2", UserLastName = "entrepreneur", Email = "Entrepreneur2@test.com"},
                new Entrepreneur{UserName = "Entrepreneur3", UserLastName = "entrepreneur", Email = "Entrepreneur3@test.com"},
                new Entrepreneur{UserName = "Entrepreneur4", UserLastName = "entrepreneur", Email = "Entrepreneur4@test.com"},
            };

                foreach (var entrepreneur in entrepreneurs)
                {
                    await userManager.CreateAsync(entrepreneur, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(entrepreneur, "Entrepreneur");
                }
            }

        }
    }
}
