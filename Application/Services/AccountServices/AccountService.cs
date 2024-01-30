using System.Security.Claims;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.AccountDTOs;
using Domain.DTOs.UserDTOs;
using DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;
    private readonly DataContext _context;
    public AccountService(UserManager<AppUser> userManager, IMapper mapper, TokenService tokenService, DataContext context)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Result<UserDto>.Failure(ResultErrorType.NotFound, $"The user with the email : {loginDto.Email} doesnt exist!");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);

            return Result<UserDto>.Success(userDto);
        };

        return Result<UserDto>.Failure(ResultErrorType.Unauthorized, $"Wrong credentials, try again!");
    }
    private bool IsValidRole(string roleName)
    {
        // Add additional roles as needed
        return roleName == "JobSeeker" || roleName == "Entrepreneur" || roleName == "Admin";
    }
    public async Task<Result<UserDto>> Register(RegisterDto registerDto, string roleName)
    {
        if (!IsValidRole(roleName))
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Invalid role name. Allowed values are JobSeeker, Entrepreneur, and Admin.");
        }
        
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email || x.UserName == registerDto.UserName))
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Email/username is taken try another one!");
        }

        AppUser newUser = roleName switch
        {
            "JobSeeker" => _mapper.Map<JobSeeker>(registerDto),
            "Entrepreneur" => _mapper.Map<Entrepreneur>(registerDto),
            "Admin" => _mapper.Map<AppUser>(registerDto),
            _ => _mapper.Map<AppUser>(registerDto)
        };

        if (newUser == null)
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity!");
        }


        var result = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, roleName);
            UserDto userDto = newUser switch
            {
                JobSeeker jobSeeker => _mapper.Map<JobSeekerDto>(jobSeeker),
                Entrepreneur entrepreneur => _mapper.Map<EntrepreneurDto>(entrepreneur),
                Recruiter recruiter => _mapper.Map<RecruiterDto>(recruiter),
                AppUser user => _mapper.Map<UserDto>(user),
                _ => null
            };

            if (userDto != null)
            {
                userDto.Token = await _tokenService.CreateToken(newUser);
                return Result<UserDto>.Success(userDto);
            }

            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Failed to register the user!");
        }

        return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Something went wrong! Try again!");
    }
    public async Task<Result<UserDto>> Register(RegisterRecruiterDto recruiterDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == recruiterDto.Email || x.UserName == recruiterDto.UserName))
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Email/username is taken, try another one!");
        }

        var newUser = _mapper.Map<Recruiter>(recruiterDto);
        newUser.CompanyId = recruiterDto.CompanyId;

        var result = await _userManager.CreateAsync(newUser, recruiterDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "Recruiter");

            var userDto = _mapper.Map<RecruiterDto>(newUser);
            userDto.Token = await _tokenService.CreateToken(newUser);

            return Result<UserDto>.Success(userDto);
        }

        return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Failed to register the recruiter. Please try again.");
    }



    public async Task<Result<UserDto>> GetCurrentUser(ClaimsPrincipal userPrincipal)
    {
        if (userPrincipal == null)
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest);
        }
        var userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(userEmail))
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "User email couldn't be found!");
        }
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            return Result<UserDto>.Failure(ResultErrorType.NotFound, "User is not found!");
        }
        var userDto = _mapper.Map<UserDto>(user);

        if (userDto == null)
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest, "Something went wrong while mapping the User to UserDto!");
        }
        userDto.Token = await _tokenService.CreateToken(user);

        return Result<UserDto>.Success(userDto);
    }


}
