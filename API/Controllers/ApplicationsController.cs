using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApplicationsController : BaseApiController
    {
        private readonly MainService _service;
        public ApplicationsController(MainService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin,Recruiter,Entrepreneur")]
        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            return HandleResult(await _service._applicationService.GetAll());
        }

        [Authorize(Roles = "Admin,Recruiter,Entrepreneur")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(Guid id)
        {

            return HandleResult(await _service._applicationService.GetById(id));
        }

        [Authorize(Roles = "Admin,Recruiter,Entrepreneur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {

            return HandleResult(await _service._applicationService.Delete(id));
        }
    }
}