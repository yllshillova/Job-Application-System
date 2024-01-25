using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;
using MediatR;

namespace Application.Services.CompanyServices
{
  public interface ICompanyService : IEntityBaseRepository<Company, CompanyDto>
  {

    Task<Result<List<CompanyDto>>> GetAllCompanies();
    Task<Result<CompanyDto>> GetCompanyById(Guid id);
    Task<Result<Unit>> AddCompany(CompanyDto companyDto);
  }
}