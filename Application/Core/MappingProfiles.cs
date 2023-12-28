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
            // User mapping
            // CreateMap<User, UserDto>().ReverseMap();
            CreateMap<JobSeeker,JobSeekerDto>().ReverseMap();
            CreateMap<Recruiter,RecruiterDto>().ReverseMap();
            CreateMap<Entrepreneur,EntrepreneurDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
             CreateMap<Company, CompanyDto>().ReverseMap()
            .ForMember(dest => dest.Recruiters, opt => opt.MapFrom(src => src.Recruiters))
            .ForMember(dest => dest.EmailNotifications, opt => opt.MapFrom(src => src.EmailNotifications))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        }
    }
}