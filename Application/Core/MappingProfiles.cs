using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.UserDTOs;
using DTOs;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
            CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            CreateMap<Recruiter, RecruiterDto>().ReverseMap();
            CreateMap<Entrepreneur, EntrepreneurDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<JobPost, JobPostDto>().ReverseMap();
            CreateMap<ResumeStorage, ResumeStorageDto>().ReverseMap();
            CreateMap<EmailNotification, EmailNotificationDto>().ReverseMap();
            CreateMap<ApplicationEntity, ApplicationDto>().ReverseMap();

        }

    }
}