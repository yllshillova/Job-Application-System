using Application.Base;
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

        public async Task<JobSeekerDto> GetJobSeekerById(Guid id)
        {
            var jobSeeker = await _context.JobSeekers
                .Include(e => e.Skills)
                .Include(e => e.Educations)
                .Include(e => e.Experiences)
                .Include(e => e.Applications)
                .FirstOrDefaultAsync(e => e.Id == id);

            var jobSeekerDto = _mapper.Map<JobSeekerDto>(jobSeeker);
            return jobSeekerDto;
        }

    }
}