using Application.Services;
using Application.Services.CompanyServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompaniesController : BaseApiController
    {
        private readonly ICompanyService _service;
        public CompaniesController(ICompanyService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            return HandleResult(await _service.GetAllCompanies());
        }



        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var userClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            return HandleResult(await _service.GetCompanyById(id));
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)
        {
           return HandleResult(await _service.Add(company));
        }


        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyDto company)
        {
            company.Id = id;
            return HandleResult(await _service.Update(id, company));
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {

            return HandleResult(await _service.Delete(id));
        }
    }
}