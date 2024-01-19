using Application.Base;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.EntrepreneurServices
{
    public class EntrepreneurService : EntityBaseRepository<Entrepreneur, EntrepreneurDto>, IEntrepreneurService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EntrepreneurService(DataContext context, IMapper mapper) : base(context, mapper)
        {
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

    }
}