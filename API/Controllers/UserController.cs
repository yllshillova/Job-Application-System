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
        [HttpGet("all-users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _service.GetAllUsers();
        }
        [HttpGet("all-recruiters")]
        public async Task<ActionResult<List<Recruiter>>> GetAllRecruiters()
        {
            return await _service.GetAllRecruiters();
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            await _service.GetUserById(id);
            return Ok();
        }


    }
}