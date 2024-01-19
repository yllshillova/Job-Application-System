using Application.Services;
using Application.Services.JobPostServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobPostsController:BaseApiController
    {
        private readonly IJobPostService _service;
        public JobPostsController(IJobPostService service)
        {
            _service = service;
            
        }

        [Authorize(Roles = "Admin,Entrepreneur,Recruiter")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobPosts()
        {
            return HandleResult(await _service.GetAll(r => r.Applications));
        }
        
        [Authorize(Roles = "Admin,Entrepreneur,Recruiter")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPostById(Guid id)
        {
            return HandleResult(await _service.GetJobPostById(id));
        }

        [Authorize(Roles = "Admin,Entrepreneur,Recruiter")]
        [HttpPost]
        public async Task<IActionResult> CreateJobPost(JobPostDto jobPostDto)
        {
            return HandleResult(await _service.Add(jobPostDto));
        }

        [Authorize(Roles = "Admin,Entrepreneur,Recruiter")]
        [HttpPut]
        public async Task<IActionResult> EditJobPost(Guid id, JobPostDto jobPostDto)
        {
            jobPostDto.Id = id;
            return HandleResult(await _service.Update(id, jobPostDto));
        }
        
        [Authorize(Roles = "Admin,Entrepreneur,Recruiter")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPost(Guid id)
        {
            return HandleResult(await _service.Delete(id));
        }


    }
}