using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Services.ApplicationServices
{
    public interface IApplicationService :IEntityBaseRepository<ApplicationEntity,ApplicationDto>
    {
        Task<Result<ApplicationDto>> SubmitApplication(Guid jobSeekerId, Guid jobPostId, IFormFile resume, Guid emailNotificationId);
    }
}