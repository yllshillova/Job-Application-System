using Application.Base;
using Application.Services.UserServices;
using AutoMapper;
using Domain;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class UserService : EntityBaseRepository<User, UserDto>, IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _context.Users
                .Include(e => e.Skills)
                .Include(e => e.Educations)
                .Include(e => e.Experiences)
                .FirstOrDefaultAsync(e => e.Id == id);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}