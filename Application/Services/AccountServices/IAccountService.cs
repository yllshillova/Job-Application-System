using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain.DTOs.AccountDTOs;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.AccountServices;

public interface IAccountService
{
    Task<Result<UserDto>> Login(LoginDto loginDto);
}
