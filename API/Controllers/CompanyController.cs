using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.CompanyServices;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _service;
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            return await _service.GetAllCompanies();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id)
        {
            var company = await _service.GetCompanyById(id);
            return company;
        }
        //TODO: guid id not being passed correctly
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)
        {
            await _service.AddCompany(company);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyDto company){
            company.Id = id;
            await _service.UpdateCompany(id,company);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.DeleteCompany(id);
            return Ok();
        }
    }
}