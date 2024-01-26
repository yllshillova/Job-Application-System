using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;
using DTOs;
using MediatR;

namespace Application.Services.JobSeekerServices
{
    public interface IJobSeekerService: IEntityBaseRepository<JobSeeker, JobSeekerDto> 
    {
        Task<Result<JobSeekerDto>> GetJobSeekerById(Guid id);
        Task<Result<Unit>> AddJobSeeker(JobSeekerDto jobSeekerDto);
    }
}