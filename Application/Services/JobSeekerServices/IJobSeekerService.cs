using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;
using DTOs;

namespace Application.Services.JobSeekerServices
{
    public interface IJobSeekerService: IEntityBaseRepository<JobSeeker, JobSeekerDto> 
    {
        Task<Result<JobSeekerDto>> GetJobSeekerById(Guid id);
    }
}