using Application.Services;
using Domain;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly MainService _service;
        public UsersController(MainService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await _service._userService.GetAllUsers();
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

            return Ok(await _service._userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _service._userService.AddUser(userDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(Guid id, UserDto userDto)
        {
            userDto.Id = id;
            await _service._userService.UpdateUser(id, userDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _service._userService.DeleteUser(id);
            return Ok();
        }

    }
}