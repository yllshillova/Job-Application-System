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
        public async Task<ActionResult<List<JobSeekerDto>>> GetAllJobSeekers()
        {
            return await _service._jobSeekerService.GetAll(u => u.Educations, u => u.Experiences, u => u.Skills);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<JobSeekerDto>> GetJobSeekerById(Guid id)
        {

            return Ok(await _service._jobSeekerService.GetJobSeekerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker(JobSeekerDto jobSeekerDto)
        {
            await _service._jobSeekerService.Add(jobSeekerDto);
            return Ok();
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
            await _service._jobSeekerService.Delete(id);
            return Ok();
        }

    }
}