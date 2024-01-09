using Application.Services;
using Application.Services.JobPostServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobPostsController:BaseApiController
    {
        private readonly JobPostService _service;
        public JobPostsController(JobPostService service)
        {
            _service = service;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobPosts()
        {
            return HandleResult(await _service.GetAll(r => r.Applications));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPostById(Guid id)
        {
            return HandleResult(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPost(JobPostDto jobPostDto)
        {
            return HandleResult(await _service.Add(jobPostDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditJobPost(Guid id, JobPostDto jobPostDto)
        {
            jobPostDto.Id = id;
            return HandleResult(await _service.Update(id, jobPostDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPost(Guid id)
        {
            return HandleResult(await _service.Delete(id));
        }


    }
}