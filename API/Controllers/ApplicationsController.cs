using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApplicationsController : BaseApiController
    {
        private readonly MainService _service;
        public ApplicationsController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            return HandleResult(await _service._applicationService.GetAll());
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetApplicationById(Guid id)
        {

            return Ok(await _service._applicationService.GetById(id));
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {

            return HandleResult(await _service._applicationService.Delete(id));
        }
    }
}