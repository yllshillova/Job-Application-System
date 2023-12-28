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
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            return await _service._companyService.GetAll(r => r.Recruiters);
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
            await _service._companyService.Add(company);

            return Ok();
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
            await _service._companyService.Delete(id);
            return Ok();
        }
    }
}