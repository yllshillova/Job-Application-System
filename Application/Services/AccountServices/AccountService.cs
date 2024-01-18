using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.DTOs.AccountDTOs;
using Domain.DTOs.UserDTOs;
using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;
    public AccountService(UserManager<User> userManager, IMapper mapper, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Result<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Result<UserDto>.Failure(ResultErrorType.NotFound);

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return Result<UserDto>.Success(userDto);
        };

        return Result<UserDto>.Failure(ResultErrorType.Unauthorized);
    }

    public async Task<Result<UserDto>> Register(RegisterDto registerDto, string roleName)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email || x.UserName == registerDto.UserName))
        {
            return Result<UserDto>.Failure(ResultErrorType.BadRequest);
        }

        User newUser = roleName switch
        {
            "JobSeeker" => _mapper.Map<JobSeeker>(registerDto),
            "Entrepreneur" => _mapper.Map<Entrepreneur>(registerDto),
            "Recruiter" => _mapper.Map<Recruiter>(registerDto),
            _ => _mapper.Map<User>(registerDto)
        };

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (result.Succeeded)
        {
            UserDto userDto = newUser switch
            {
                JobSeeker jobSeeker => _mapper.Map<JobSeekerDto>(jobSeeker),
                Entrepreneur entrepreneur => _mapper.Map<EntrepreneurDto>(entrepreneur),
                Recruiter recruiter => _mapper.Map<RecruiterDto>(recruiter),
                _ => null 
            };

            if (userDto != null)
            {
                userDto.Token = _tokenService.CreateToken(newUser);
                return Result<UserDto>.Success(userDto);
            }

            return Result<UserDto>.Failure(ResultErrorType.BadRequest);
        }

        return Result<UserDto>.Failure(ResultErrorType.BadRequest);
    }
}
