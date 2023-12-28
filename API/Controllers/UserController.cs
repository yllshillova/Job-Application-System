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
            return await _service._userService.GetAll(u => u.Educations, u => u.Experiences, u => u.Skills);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetById(Guid id)
        {

            return Ok(await _service._userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _service._userService.Add(userDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(Guid id, UserDto userDto)
        {
            userDto.Id = id;
            await _service._userService.Update(id, userDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _service._userService.Delete(id);
            return Ok();
        }

    }
}