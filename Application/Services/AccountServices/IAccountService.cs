using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Core;
using Domain.DTOs.AccountDTOs;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.AccountServices;

public interface IAccountService
{
    Task<Result<UserDto>> Login(LoginDto loginDto);
    Task<Result<UserDto>> Register(RegisterDto registerDto,string roleName);
    Task<Result<UserDto>> GetCurrentUser(ClaimsPrincipal userPrincipal);
}
