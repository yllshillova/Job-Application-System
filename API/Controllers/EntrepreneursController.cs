using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.EntrepreneurServices;
using Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EntrepreneursController : BaseApiController
    {
        private readonly IEntrepreneurService _service;
        public EntrepreneursController(IEntrepreneurService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet]
        public async Task<IActionResult> GetAllEntrepreneurs()
        {
            return HandleResult(await _service.GetAllEntrepreneurs());
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntrepreneurById(Guid id)
        {
            return HandleResult(await _service.GetEntrepreneurById(id));
        }
        
        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpPost]
        public async Task<IActionResult> AddEntrepreneur(EntrepreneurDto entrepreneurDto)
        {
            
            return HandleResult(await _service.AddEntrepreneur(entrepreneurDto));
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpPut]
        public async Task<IActionResult> EditEntrepreneur(Guid id, EntrepreneurDto entrepreneurDto)
        {
            entrepreneurDto.Id = id;
            
            return HandleResult(await _service.Update(id, entrepreneurDto));
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrepreneur(Guid id)
        {
            
            return HandleResult(await _service.Delete(id));
        }



    }
}