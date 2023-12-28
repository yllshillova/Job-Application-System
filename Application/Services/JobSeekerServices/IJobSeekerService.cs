using Application.Base;
using Domain;
using Domain.DTOs;
using DTOs;

namespace Application.Services.JobSeekerServices
{
    public interface IJobSeekerService: IEntityBaseRepository<JobSeeker, JobSeekerDto> 
    {
        Task<JobSeekerDto> GetJobSeekerById(Guid id);
    }
}