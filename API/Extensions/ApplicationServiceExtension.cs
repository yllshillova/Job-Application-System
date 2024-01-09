using System.Text.Json.Serialization;
using Application.Core;
using Application.Services;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
using Application.Services.EmailNotificationServices;
using Application.Services.EntrepreneurServices;
using Application.Services.JobPostServices;
using Application.Services.JobSeekerServices;
using Application.Services.RecruiterServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<MainService>();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
           
            return services;
        }
    }
}