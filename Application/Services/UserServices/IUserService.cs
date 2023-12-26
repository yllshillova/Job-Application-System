using Domain;
using DTOs;

namespace Application.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        // Task<List<Recruiter>> GetAllRecruiters();
        //  Task<List<Skill>> GetAllSkills();
        Task<UserDto> GetUserById(Guid id);
        Task AddUser(UserDto user);
        Task UpdateUser(Guid id, UserDto updatedUserDto);
        Task DeleteUser(Guid id);
    }
}