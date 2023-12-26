using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Application.Services.CompanyServices
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(Guid id);
        Task AddCompany(CompanyDto companyDto);
        Task UpdateCompany(Guid id, CompanyDto updatedCompanyDto);
        Task DeleteCompany(Guid id);

    }
}