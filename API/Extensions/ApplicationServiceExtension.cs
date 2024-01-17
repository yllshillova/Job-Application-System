using System.Text;
using System.Text.Json.Serialization;
using Application.Core;
using Application.Services;
using Application.Services.AccountServices;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
using Application.Services.EmailNotificationServices;
using Application.Services.EntrepreneurServices;
using Application.Services.JobPostServices;
using Application.Services.JobSeekerServices;
using Application.Services.RecruiterServices;
using Domain;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IJobSeekerService, JobSeekerService>();
            services.AddScoped<IRecruiterService, RecruiterService>();
            services.AddScoped<IEntrepreneurService, EntrepreneurService>();
            services.AddScoped<IJobPostService, JobPostService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<MainService>();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DataContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Iww4kxaMt6GzDUj4vbqnJPXgIadhgeNMvBr55NNjga7HBvDGzajJ8YbN8ecMeCZe"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });


            services.AddScoped<TokenService>();
            return services;
        }
    }
}