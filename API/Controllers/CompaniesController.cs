using Application.Services;
using Application.Services.CompanyServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompaniesController : BaseApiController
    {
        private readonly CompanyService _service;
        public CompaniesController(CompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            return HandleResult(await _service.GetAllCompanies());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            return HandleResult(await _service.GetCompanyById(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)
        {
            return HandleResult( await _service.Add(company));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyDto company){
            company.Id = id;
            await _service.Update(id,company);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            
            return HandleResult(await _service.Delete(id));
        }
    }
}