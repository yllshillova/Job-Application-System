using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs;
using MediatR;

namespace Application.Services.RecruiterServices
{
    public interface IRecruiterService: IEntityBaseRepository<Recruiter,RecruiterDto>
    {
        Task<Result<RecruiterDto>> GetRecruiterById(Guid id);
        Task<Result<Unit>> AddRecruiter(RecruiterDto recruiterDto);
    }
}