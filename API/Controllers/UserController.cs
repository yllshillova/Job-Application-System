using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.UserServices;
using Domain;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _service.GetAllUsers();
        }
        // [HttpGet("recruiters")]
        // public async Task<ActionResult<List<Recruiter>>> GetAllRecruiters()
        // {
        //     return await _service.GetAllRecruiters();
        // }
        // [HttpGet("skills")]
        // public async Task<ActionResult<List<Skill>>> GetAllSkills(){
        //     return await _service.GetAllSkills();
        // }


        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUserById(Guid id)
        {

            return Ok(await _service.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _service.AddUser(userDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(Guid id, UserDto userDto)
        {
            userDto.Id = id;
            await _service.UpdateUser(id, userDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _service.DeleteUser(id);
            return Ok();
        }

    }
}