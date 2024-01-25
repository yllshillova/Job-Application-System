using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.CompanyServices
{
    public class CompanyService : EntityBaseRepository<Company, CompanyDto>, ICompanyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CompanyService(DataContext context, IMapper mapper, IEmailService emailService) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
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
            .ToListAsync();

            if (companies == null || companies.Count == 0) return Result<List<CompanyDto>>.Failure(ResultErrorType.NotFound, "No companies could be found!");
            var companydtos = _mapper.Map<List<CompanyDto>>(companies);

            return Result<List<CompanyDto>>.Success(companydtos);
        }

        public async Task<Result<CompanyDto>> GetCompanyById(Guid id)
        {
            var company = await _context.Companies
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Educations)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Skills)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.Experiences)
            .Include(c => c.Recruiters)
                .ThenInclude(r => r.JobPosts)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null) return Result<CompanyDto>.Failure(ResultErrorType.NotFound, $"No company with id {id} could be found!");
            var companydto = _mapper.Map<CompanyDto>(company);

            if (companydto == null)
            {
                return Result<CompanyDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }
            return Result<CompanyDto>.Success(companydto);

        }

        public async Task<Result<Unit>> AddCompany(CompanyDto companyDto)
        {
            var addCompanyResult = await Add(companyDto);

            if (addCompanyResult.IsSuccess)
            {
                string companyEmail = companyDto.Email;
                string companyName = companyDto.Name;

                await AddSenderToMailjet(companyEmail, companyName);
                return Result<Unit>.Success(Unit.Value);
            }
            return Result<Unit>.Failure(ResultErrorType.BadRequest, "Failed to add the sender to the mailjet");

        }


        private async Task AddSenderToMailjet(string email, string name)
        {
            await _emailService.AddSenderToMailjet(email, name);
        }




        // public async Task<Result<Unit>> AddCompanyAndContact(CompanyDto companyDto, string mailjetApiKey, string mailjetApiSecret)
        // {
        //     var addCompanyResult = await Add(companyDto);

        //     if (addCompanyResult.IsSuccess)
        //     {
        //         string companyEmail = companyDto.Email;

        //         // Use the injected MailjetService to add the company's email to Mailjet contact list
        //         var addContactResult = await _emailService.AddContactToList(mailjetApiKey, mailjetApiSecret, companyEmail);

        //         if (addContactResult.IsSuccess)
        //         {
        //             return Result<Unit>.Success(Unit.Value);
        //         }
        //         else
        //         {
        //             // Handle the case where adding to Mailjet failed
        //             return Result<Unit>.Failure(addContactResult.ErrorType, addContactResult.ErrorMessage);
        //         }
        //     }
        //     else
        //     {
        //         // Handle the case where adding the company to the database failed
        //         return Result<Unit>.Failure(addCompanyResult.ErrorType, addCompanyResult.ErrorMessage);
        //     }
        // }


    }
}