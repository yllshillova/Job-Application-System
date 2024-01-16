using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs.AccountDTOs;
using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public AccountService(UserManager<User> userManager, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Result<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Result<UserDto>.Failure(ResultErrorType.NotFound);

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {
            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto);
        }
        return Result<UserDto>.Failure(ResultErrorType.Unauthorized);
    }
}
