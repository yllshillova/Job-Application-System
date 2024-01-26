using Application.Base;
using Application.Core;
using Application.Services.AccountServices;
using Application.Services.JobSeekerServices;
using AutoMapper;
using Domain;
using Domain.DTOs;
using DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class JobSeekerService : EntityBaseRepository<JobSeeker, JobSeekerDto>, IJobSeekerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        public JobSeekerService(DataContext context, IMapper mapper, UserManager<AppUser> userManager, TokenService tokenService) : base(context, mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<JobSeekerDto>> GetJobSeekerById(Guid id)
        {
            var jobSeeker = await _context.Users.OfType<JobSeeker>()
                .Include(e => e.Skills)
                .Include(e => e.Educations)
                .Include(e => e.Experiences)
                .Include(e => e.Applications)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (jobSeeker == null) return Result<JobSeekerDto>.Failure(ResultErrorType.NotFound, $"No jobSeeker with id {id} could be found!");
            var jobSeekerDto = _mapper.Map<JobSeekerDto>(jobSeeker);

            if (jobSeekerDto == null)
            {
                return Result<JobSeekerDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }

            return Result<JobSeekerDto>.Success(jobSeekerDto);
        }
        public async Task<Result<Unit>> AddJobSeeker(JobSeekerDto jobSeekerDto)
        {
            var jobSeeker = _mapper.Map<JobSeeker>(jobSeekerDto);
            if (jobSeeker == null)
            {
                return Result<Unit>.Failure(ResultErrorType.BadRequest, $"Problem while mapping from/to {typeof(JobSeekerDto).Name}");
            }


            var result = await _userManager.CreateAsync(jobSeeker, jobSeekerDto.PasswordHash);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(jobSeeker, "JobSeeker");

                // You can map the user to the appropriate DTO based on the role
                JobSeekerDto mappedJobSeekerDto = _mapper.Map<JobSeekerDto>(jobSeeker);

                if (mappedJobSeekerDto != null)
                {
                    mappedJobSeekerDto.Token = await _tokenService.CreateToken(jobSeeker);
                    return Result<Unit>.Success(Unit.Value);
                }
            }

            return Result<Unit>.Failure(ResultErrorType.BadRequest, "Failed to register the user!");
        }

    }
}