using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
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

        public async Task<RecruiterDto> GetRecruiterById(Guid id)
        {
            var recruiter = await _context.Recruiters
            .Include(r => r.Educations)
            .Include(r => r.Experiences)
            .Include(r => r.Skills)
            .Include(r => r.JobPosts)
            .FirstOrDefaultAsync(r => r.Id == id);

            var recruiterDto = _mapper.Map<RecruiterDto>(recruiter);

            return recruiterDto;
        }
    }
}