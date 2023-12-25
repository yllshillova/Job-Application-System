using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;

namespace Application.Services.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        // Task<List<Recruiter>> GetAllRecruiters();
        Task<User> GetUserById(Guid id);
        Task AddUser(User user);
        Task UpdateUser(Guid id, User updatedUser);
        Task DeleteUser(Guid id);
    }
}