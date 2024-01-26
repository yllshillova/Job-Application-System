using Application.Base;
using Application.Core;
using Application.Services.AccountServices;
using AutoMapper;
using Domain;
using Domain.DTOs.UserDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.EntrepreneurServices
{
    public class EntrepreneurService : EntityBaseRepository<Entrepreneur, EntrepreneurDto>, IEntrepreneurService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        public EntrepreneurService(DataContext context, IMapper mapper, UserManager<AppUser> userManager, TokenService tokenService) : base(context, mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<EntrepreneurDto>>> GetAllEntrepreneurs()
        {
            var entrepreneurs = await _context.Users.OfType<Entrepreneur>()
                .Include(e => e.Skills)
                .Include(e => e.Educations)
                .Include(e => e.Experiences)
                .Include(e => e.Companies)
                    .ThenInclude(c => c.Recruiters)
                    .ThenInclude(r => r.Educations)
                .Include(e => e.Companies)
                    .ThenInclude(c => c.Recruiters)
                    .ThenInclude(r => r.Skills)
                .Include(e => e.Companies)
                    .ThenInclude(c => c.Recruiters)
                    .ThenInclude(r => r.Experiences)
                .Include(e => e.Companies)
                    .ThenInclude(c => c.Recruiters)
                    .ThenInclude(r => r.JobPosts)
                .ToListAsync();

            if (entrepreneurs == null || entrepreneurs.Count == 0) return Result<List<EntrepreneurDto>>.Failure(ResultErrorType.NotFound, "No entrepreneurs couldn't be found!");

            var entrepreneurDtos = _mapper.Map<List<EntrepreneurDto>>(entrepreneurs);
            return Result<List<EntrepreneurDto>>.Success(entrepreneurDtos);
        }
        public async Task<Result<EntrepreneurDto>> GetEntrepreneurById(Guid id)
        {
            var entrepreneur = await _context.Users.OfType<Entrepreneur>()
                .Include(e => e.Skills)
                .Include(e => e.Educations)
                .Include(e => e.Experiences)
                .Include(e => e.Companies)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entrepreneur == null) return Result<EntrepreneurDto>.Failure(ResultErrorType.NotFound, $"No entrepreneur with id {id} could be found!");
            var entrepreneurDto = _mapper.Map<EntrepreneurDto>(entrepreneur);

            if (entrepreneurDto == null)
            {
                return Result<EntrepreneurDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }

            return Result<EntrepreneurDto>.Success(entrepreneurDto);
        }

        public async Task<Result<Unit>> AddEntrepreneur(EntrepreneurDto entrepreneurDto)
        {
            var entrepreneur = _mapper.Map<Entrepreneur>(entrepreneurDto);
            if (entrepreneur == null)
            {
                return Result<Unit>.Failure(ResultErrorType.BadRequest, $"Problem while mapping from/to {typeof(EntrepreneurDto).Name}");
            }


            var result = await _userManager.CreateAsync(entrepreneur, entrepreneurDto.PasswordHash);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(entrepreneur, "Entrepreneur");

                // You can map the user to the appropriate DTO based on the role
                EntrepreneurDto mappedEntrepreneurDto = _mapper.Map<EntrepreneurDto>(entrepreneur);

                if (mappedEntrepreneurDto != null)
                {
                    mappedEntrepreneurDto.Token = await _tokenService.CreateToken(entrepreneur);
                    return Result<Unit>.Success(Unit.Value);
                }
            }

            return Result<Unit>.Failure(ResultErrorType.BadRequest, "Failed to register the user!");
        }


    }
}