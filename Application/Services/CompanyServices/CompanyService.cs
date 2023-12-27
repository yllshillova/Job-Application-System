using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CompanyService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CompanyDto>> GetAllCompanies()
        {
            var companies = await _context.Companies
            .ToListAsync();

            var companyDtos = _mapper.Map<List<CompanyDto>>(companies);

            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyById(Guid id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task AddCompany(CompanyDto companyDto)
        {
            var entrepreneur = await _context.Entrepreneurs.FindAsync(companyDto.EntrepreneurId);

            if(entrepreneur == null) throw new Exception("Entrpreneur not found");

            var company = _mapper.Map<Company>(companyDto);

            company.EntrepreneurId = entrepreneur.Id;


            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCompany(Guid id, CompanyDto updatedCompanyDto)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null) return;

            _mapper.Map(updatedCompanyDto, company);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteCompany(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return;
        
            _context.Remove(company);
            await _context.SaveChangesAsync();
        }



    }
}