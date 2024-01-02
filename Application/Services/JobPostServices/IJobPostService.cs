using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;

namespace Application.Services.JobPostServices
{
    public interface IJobPostService: IEntityBaseRepository<JobPost,JobPostDto>
    {
        Task<Result<JobPostDto>> GetJobPostById(Guid id);
    }
}