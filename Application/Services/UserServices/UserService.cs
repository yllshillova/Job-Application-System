using Application.Services.UserServices;
using AutoMapper;
using Domain;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _context.Users
             .Include(u => u.Skills)
             .Include(u => u.Educations)
             .Include(u => u.Experiences)
             .ToListAsync();

            var userDtos = _mapper.Map<List<UserDto>>(users);

            return userDtos;

        }
        public async Task<List<Recruiter>> GetAllRecruiters()
        {
            return await _context.Recruiters.ToListAsync();
        }

        // public async Task<List<Skill>> GetAllSkills()
        // {
        //     return await _context.Skills.ToListAsync();
        // }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _context.Users.
            Include(u => u.Skills)
            .Include(u => u.Educations)
            .Include(u => u.Experiences).FirstOrDefaultAsync(x => x.Id == id);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;

        }
        public async Task AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateUser(Guid id, UserDto updatedUserDto)
        {
            var user = await _context.Users
            .Include(u => u.Educations)
            .Include(u => u.Experiences)
            .Include(u => u.Skills)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)  return;

            _mapper.Map(updatedUserDto, user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null) return;

            _context.Remove(user);
            
            await _context.SaveChangesAsync();
        }
    }
}