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

            var entrepreneurDto = _mapper.Map<EntrepreneurDto>(entrepreneur);
            return Result<EntrepreneurDto>.Success(entrepreneurDto);
        }

    }
}