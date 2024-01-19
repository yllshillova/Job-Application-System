using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.RecruiterServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecruitersController : BaseApiController
    {
        private readonly IRecruiterService _service;
        public RecruitersController(IRecruiterService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllRecruiters()
        {
            return HandleResult(await _service.GetAll(r => r.Educations, r => r.Experiences, r => r.Skills, r => r.JobPosts));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruiterById(Guid id)
        {
            return HandleResult(await _service.GetRecruiterById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRecruiter(RecruiterDto recruiterDto)
        {

            return HandleResult(await _service.Add(recruiterDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditRecruiter(Guid id, RecruiterDto updatedRecruiterDto)
        {
            updatedRecruiterDto.Id = id;

            return HandleResult(await _service.Update(id, updatedRecruiterDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruiter(Guid id)
        {

            return HandleResult(await _service.Delete(id));
        }

    }
}