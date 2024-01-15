using Application.Services;
using Domain;
using Domain.DTOs;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobSeekersController : BaseApiController
    {
        private readonly MainService _service;
        public JobSeekersController(MainService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllJobSeekers()
        {
            return HandleResult(await _service._jobSeekerService.GetAll(u => u.Educations, u => u.Experiences, u => u.Skills));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetJobSeekerById(Guid id)
        {

            return Ok(await _service._jobSeekerService.GetJobSeekerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker(JobSeekerDto jobSeekerDto)
        {

            return HandleResult(await _service._jobSeekerService.Add(jobSeekerDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditJobSeeker(Guid id, JobSeekerDto jobSeekerDto)
        {
            jobSeekerDto.Id = id;
            await _service._jobSeekerService.Update(id, jobSeekerDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(Guid id)
        {

            return HandleResult(await _service._jobSeekerService.Delete(id));
        }

        [HttpPost("submitApplication")]
        public async Task<IActionResult> SubmitApplication(Guid jobSeekerId, Guid jobPostId, IFormFile resume, Guid emailNotificationId)
        {
            return HandleResult(await _service._applicationService.SubmitApplication(jobSeekerId, jobPostId, resume, emailNotificationId));
        }

    }
}