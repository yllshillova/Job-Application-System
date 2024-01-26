using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Domain;
using Domain.DTOs.UserDTOs;
using MediatR;

namespace Application.Services.EntrepreneurServices
{
    public interface IEntrepreneurService: IEntityBaseRepository<Entrepreneur,EntrepreneurDto>
    {
        Task<Result<List<EntrepreneurDto>>> GetAllEntrepreneurs();
        Task<Result<EntrepreneurDto>> GetEntrepreneurById(Guid id);
        Task<Result<Unit>> AddEntrepreneur(EntrepreneurDto entrepreneurDto);
    }
}