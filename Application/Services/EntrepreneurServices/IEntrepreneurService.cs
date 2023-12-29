using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Domain;
using Domain.DTOs.UserDTOs;

namespace Application.Services.EntrepreneurServices
{
    public interface IEntrepreneurService: IEntityBaseRepository<Entrepreneur,EntrepreneurDto>
    {
        Task<EntrepreneurDto> GetEntrepreneurById(Guid id);
    }
}