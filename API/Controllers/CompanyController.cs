using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.CompanyServices;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompanyController : BaseApiController
    {
        private readonly MainService _service;
        public CompanyController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            return await _service._companyService.GetAllCompanies();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id)
        {
            var company = await _service._companyService.GetCompanyById(id);
            return company;
        }
        //TODO: guid id not being passed correctly
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)
        {
            await _service._companyService.AddCompany(company);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyDto company){
            company.Id = id;
            await _service._companyService.UpdateCompany(id,company);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service._companyService.DeleteCompany(id);
            return Ok();
        }
    }
}