using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EntrepreneursController : BaseApiController
    {
        private readonly MainService _service;
        public EntrepreneursController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntrepreneurDto>>> GetAllEntrepreneurs()
        {
            return await _service._entrepreneurService.GetAll(e => e.Companies, e => e.Educations, e => e.Experiences, e => e.Skills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntrepreneurDto>> GetEntrepreneurById(Guid id)
        {
            return await _service._entrepreneurService.GetEntrepreneurById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddEntrepreneur(EntrepreneurDto entrepreneurDto)
        {
            await _service._entrepreneurService.Add(entrepreneurDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEntrepreneur(Guid id, EntrepreneurDto entrepreneurDto)
        {
            entrepreneurDto.Id = id;
            await _service._entrepreneurService.Update(id, entrepreneurDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrepreneur(Guid id)
        {
            await _service._entrepreneurService.Delete(id);
            return Ok();
        }



    }
}