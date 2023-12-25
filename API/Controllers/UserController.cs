using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.UserServices;
using Domain;
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
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _service.GetAllUsers();
        }
        // [HttpGet("recruiters")]
        // public async Task<ActionResult<List<Recruiter>>> GetAllRecruiters()
        // {
        //     return await _service.GetAllRecruiters();
        // }

        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUserById(Guid id)
        {

            return Ok(await _service.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _service.AddUser(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(Guid id, User user)
        {
            user.Id = id;
            await _service.UpdateUser(id, user);
            return Ok();
        }
          [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await _service.DeleteUser(id);
            return Ok();
        }

    }
}