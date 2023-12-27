using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.DTOs;

namespace Application.Services.CompanyServices
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(Guid id);
        Task AddCompany(CompanyDto company);
        Task UpdateCompany(Guid id, CompanyDto updatedCompany);
        Task DeleteCompany(Guid id);

    }
}