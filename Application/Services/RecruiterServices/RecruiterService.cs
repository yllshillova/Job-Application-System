using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Application.Services.AccountServices;
using AutoMapper;
using Domain;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.RecruiterServices
{
    public class RecruiterService : EntityBaseRepository<Recruiter, RecruiterDto>, IRecruiterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        public RecruiterService(DataContext context, IMapper mapper, UserManager<AppUser> userManager,TokenService tokenService) : base(context, mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<RecruiterDto>> GetRecruiterById(Guid id)
        {
            var recruiter = await _context.Users.OfType<Recruiter>()
            .Include(r => r.Educations)
            .Include(r => r.Experiences)
            .Include(r => r.Skills)
            .Include(r => r.JobPosts)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (recruiter == null) return Result<RecruiterDto>.Failure(ResultErrorType.NotFound, $"No recruiter with id {id} could be found!");

            var recruiterDto = _mapper.Map<RecruiterDto>(recruiter);
            if (recruiterDto == null)
            {
                return Result<RecruiterDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }

            return Result<RecruiterDto>.Success(recruiterDto);
        }

        public async Task<Result<Unit>> AddRecruiter(RecruiterDto recruiterDto)
        {
            var recruiter = _mapper.Map<Recruiter>(recruiterDto);
            if (recruiter == null)
            {
                return Result<Unit>.Failure(ResultErrorType.BadRequest, $"Problem while mapping from/to {typeof(RecruiterDto).Name}");
            }


            var result = await _userManager.CreateAsync(recruiter, recruiterDto.PasswordHash);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(recruiter, "Recruiter");

                // You can map the user to the appropriate DTO based on the role
                RecruiterDto mappedRecruiterDto = _mapper.Map<RecruiterDto>(recruiter);

                if (mappedRecruiterDto != null)
                {
                    mappedRecruiterDto.Token = await _tokenService.CreateToken(recruiter);
                    return Result<Unit>.Success(Unit.Value);
                }
            }

            return Result<Unit>.Failure(ResultErrorType.BadRequest, "Failed to register the user!");
        }

    }
}