using Application.Base;
using Domain;
using DTOs;

namespace Application.Services.UserServices
{
    public interface IUserService: IEntityBaseRepository<User, UserDto> 
    {
        Task<UserDto> GetUserById(Guid id);
    }
}