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
        public async Task<ActionResult<List<RecruiterDto>>> GetAllRecruiters()
        {
            return await _service._recruiterService.GetAll(r => r.Educations, r => r.Experiences, r => r.Skills, r => r.JobPosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecruiterDto>> GetRecruiterById(Guid id)
        {
            return await _service._recruiterService.GetRecruiterById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecruiter(RecruiterDto recruiterDto)
        {
            await _service._recruiterService.Add(recruiterDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditRecruiter(Guid id, RecruiterDto updatedRecruiterDto){
            updatedRecruiterDto.Id = id;
            await _service._recruiterService.Update(id, updatedRecruiterDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecruiter(Guid id)
        {
            await _service._recruiterService.Delete(id);
            return Ok();
        }

    }
}