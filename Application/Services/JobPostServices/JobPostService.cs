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

            if (jobPost == null) return Result<JobPostDto>.Failure(ResultErrorType.NotFound, $"No jobPost with id {id} could be found!");

            var jobPostDto = _mapper.Map<JobPostDto>(jobPost);

            if (jobPostDto == null)
            {
                return Result<JobPostDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }

            return Result<JobPostDto>.Success(jobPostDto);

        }
    }
}