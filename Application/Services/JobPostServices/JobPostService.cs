using Application.Base;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.JobPostServices
{
    public class JobPostService : EntityBaseRepository<JobPost, JobPostDto>, IJobPostService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public JobPostService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<JobPostDto>> GetJobPostById(Guid id)
        {
            var jobPost = await _context.JobPosts
            .Include(j => j.Applications).FirstOrDefaultAsync(x => x.Id == id);

            var jobPostDto = _mapper.Map<JobPostDto>(jobPost);

            return Result<JobPostDto>.Success(jobPostDto);

        }
    }
}