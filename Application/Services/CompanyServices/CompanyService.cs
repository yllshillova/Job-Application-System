using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.CompanyServices
{
    public class CompanyService : EntityBaseRepository<Company, CompanyDto>, ICompanyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CompanyService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<CompanyDto>>> GetAllCompanies()
        {
            var companies = await _context.Companies
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Educations)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Skills)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Experiences)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.JobPosts)
            .Include(c => c.EmailNotifications)
            .ToListAsync();

            if (companies == null || companies.Count == 0) return Result<List<CompanyDto>>.Failure(ResultErrorType.NotFound, "No companies couldn't be found!");
            var companydtos = _mapper.Map<List<CompanyDto>>(companies);

            return Result<List<CompanyDto>>.Success(companydtos);
        }

        public async Task<Result<CompanyDto>> GetCompanyById(Guid id)
        {
            var company = await _context.Companies
            .Include(c => c.Recruiters)
            .Include(c => c.EmailNotifications)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null) return Result<CompanyDto>.Failure(ResultErrorType.NotFound, $"No company with id {id} could be found!");
            var companydto = _mapper.Map<CompanyDto>(company);

            if (companydto == null)
            {
                return Result<CompanyDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }
            return Result<CompanyDto>.Success(companydto);

        }
    }
}