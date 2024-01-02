using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobPostsController:BaseApiController
    {
        private readonly MainService _service;
        public JobPostsController(MainService service)
        {
            _service = service;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobPosts()
        {
            return HandleResult(await _service._jobPostService.GetAll(r => r.Applications));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPostById(Guid id)
        {
            return HandleResult(await _service._jobPostService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPost(JobPostDto jobPostDto)
        {
            return HandleResult(await _service._jobPostService.Add(jobPostDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditJobPost(Guid id, JobPostDto jobPostDto)
        {
            jobPostDto.Id = id;
            return HandleResult(await _service._jobPostService.Update(id, jobPostDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPost(Guid id)
        {
            return HandleResult(await _service._jobPostService.Delete(id));
        }


    }
}