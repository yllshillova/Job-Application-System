using Application.Base;
using Application.Core;
using Application.Services.JobSeekerServices;
using AutoMapper;
using Domain;
using Domain.DTOs;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class JobSeekerService : EntityBaseRepository<JobSeeker, JobSeekerDto>, IJobSeekerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public JobSeekerService(DataContext context, IMapper mapper) : base(context, mapper)
        {
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


    }
}