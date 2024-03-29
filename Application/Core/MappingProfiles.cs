using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.AccountDTOs;
using Domain.DTOs.UserDTOs;
using DTOs;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<RegisterDto, AppUser>().ReverseMap();
            CreateMap<RegisterDto, JobSeeker>().ReverseMap();
            CreateMap<RegisterDto, Recruiter>().ReverseMap();
            CreateMap<RegisterDto, Entrepreneur>().ReverseMap();

            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
            CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            CreateMap<Recruiter, RecruiterDto>().ReverseMap();
            CreateMap<Entrepreneur, EntrepreneurDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<JobPost, JobPostDto>().ReverseMap();
            CreateMap<ResumeStorage, ResumeStorageDto>().ReverseMap();
            CreateMap<ApplicationEntity, ApplicationDto>().ReverseMap();

        }

    }
}