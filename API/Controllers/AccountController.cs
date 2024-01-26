using System.Security.Claims;
using Application.Services.AccountServices;
using Domain.DTOs.AccountDTOs;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseApiController
{
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
                _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
                return HandleResult(await _service.Login(loginDto));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, string roleName)
        {
                return HandleResult(await _service.Register(registerDto, roleName));
        }
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
                return HandleResult(await _service.GetCurrentUser(User));
        }


}
