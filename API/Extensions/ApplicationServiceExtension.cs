using System.Text.Json.Serialization;
using Application.Core;
using Application.Services;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
using Application.Services.JobPostServices;
using Application.Services.JobSeekerServices;
using Application.Services.RecruiterServices;
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
            // services.AddControllers().AddJsonOptions(options =>
            // {
            //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            // });
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IJobSeekerService, JobSeekerService>();
            services.AddScoped<IJobPostService, JobPostService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRecruiterService, RecruiterService>();
            services.AddScoped<MainService>();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            return services;
        }
    }
}