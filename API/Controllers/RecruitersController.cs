using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecruitersController : BaseApiController
    {
        private readonly MainService _service;
        public RecruitersController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecruiters()
        {
            return HandleResult(await _service._recruiterService.GetAll(r => r.Educations, r => r.Experiences, r => r.Skills, r => r.JobPosts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruiterById(Guid id)
        {
            return HandleResult(await _service._recruiterService.GetRecruiterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddRecruiter(RecruiterDto recruiterDto)
        {
            
            return HandleResult(await _service._recruiterService.Add(recruiterDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditRecruiter(Guid id, RecruiterDto updatedRecruiterDto){
            updatedRecruiterDto.Id = id;
            await _service._recruiterService.Update(id, updatedRecruiterDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruiter(Guid id)
        {
            
            return HandleResult(await _service._recruiterService.Delete(id));
        }

    }
}