using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Result<CompanyDto>> GetCompanyById(Guid id)
        {
            var company = await _context.Companies
            .Include(c => c.Recruiters)
            .Include(c => c.EmailNotifications)
            .FirstOrDefaultAsync(c => c.Id == id);

            var companydto = _mapper.Map<CompanyDto>(company);

            return Result<CompanyDto>.Success(companydto);

        }
    }
}