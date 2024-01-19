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

namespace Application.Services.RecruiterServices
{
    public class RecruiterService : EntityBaseRepository<Recruiter, RecruiterDto>, IRecruiterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RecruiterService(DataContext context, IMapper mapper) : base(context, mapper)
        {
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
    }
}