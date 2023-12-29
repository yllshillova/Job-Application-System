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
        public async Task<IActionResult> GetAllEntrepreneurs()
        {
            return HandleResult(await _service._entrepreneurService.GetAll(e => e.Companies, e => e.Educations, e => e.Experiences, e => e.Skills));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntrepreneurById(Guid id)
        {
            return HandleResult(await _service._entrepreneurService.GetEntrepreneurById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddEntrepreneur(EntrepreneurDto entrepreneurDto)
        {
            
            return HandleResult(await _service._entrepreneurService.Add(entrepreneurDto));
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
            
            return HandleResult(await _service._entrepreneurService.Delete(id));
        }



    }
}