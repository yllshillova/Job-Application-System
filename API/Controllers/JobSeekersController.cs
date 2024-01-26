using Application.Services;
using Domain;
using Domain.DTOs;
using DTOs;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobSeekers()
        {
            return HandleResult(await _service._jobSeekerService.GetAll(u => u.Educations, u => u.Experiences, u => u.Skills, u => u.Applications));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobSeekerById(Guid id)
        {

            return HandleResult(await _service._jobSeekerService.GetJobSeekerById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker(JobSeekerDto jobSeekerDto)
        {

            return HandleResult(await _service._jobSeekerService.AddJobSeeker(jobSeekerDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditJobSeeker(Guid id, JobSeekerDto jobSeekerDto)
        {
            jobSeekerDto.Id = id;

            return HandleResult(await _service._jobSeekerService.Update(id, jobSeekerDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(Guid id)
        {

            return HandleResult(await _service._jobSeekerService.Delete(id));
        }

        [Authorize(Roles = "JobSeeker,Recruiter")]
        [HttpPost("submitApplication")]
        public async Task<IActionResult> SubmitApplication(Guid jobSeekerId, Guid jobPostId, IFormFile resume)
        {
            return HandleResult(await _service._applicationService.SubmitApplication(jobSeekerId, jobPostId, resume));
        }

    }
}