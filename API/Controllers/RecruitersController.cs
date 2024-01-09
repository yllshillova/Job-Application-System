using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.RecruiterServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecruitersController : BaseApiController
    {
        private readonly RecruiterService _service;
        public RecruitersController(RecruiterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecruiters()
        {
            return HandleResult(await _service.GetAll(r => r.Educations, r => r.Experiences, r => r.Skills, r => r.JobPosts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruiterById(Guid id)
        {
            return HandleResult(await _service.GetRecruiterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddRecruiter(RecruiterDto recruiterDto)
        {
            
            return HandleResult(await _service.Add(recruiterDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditRecruiter(Guid id, RecruiterDto updatedRecruiterDto){
            updatedRecruiterDto.Id = id;
            await _service.Update(id, updatedRecruiterDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruiter(Guid id)
        {
            
            return HandleResult(await _service.Delete(id));
        }

    }
}