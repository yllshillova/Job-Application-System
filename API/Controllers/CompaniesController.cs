using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompaniesController : BaseApiController
    {
        private readonly MainService _service;
        public CompaniesController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            return HandleResult(await _service._companyService.GetAll(r => r.Recruiters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            return HandleResult(await _service._companyService.GetCompanyById(id));
        }
        //TODO: guid id not being passed correctly
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)
        {
            return HandleResult( await _service._companyService.Add(company));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyDto company){
            company.Id = id;
            await _service._companyService.Update(id,company);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            
            return HandleResult(await _service._companyService.Delete(id));
        }
    }
}