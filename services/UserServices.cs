using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services.Interfaces;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using UserApi.Dto;


namespace UserApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly ServerDbContext _context;
        public UserServices(ServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _context.Users
                .Join(_context.Roles,
                user => user.RoleId,
                role => role.Id,
                (user, role)=> new UserDto 
                {
                    Id = user.Id,
                    Name = user.Name,
                    Voucher = user.Voucher,
                    RoleName = role.Name,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                }).ToListAsync();
        }
        public async Task<User> GetUser(long id)
        {
            return await _context.Users.FindAsync(id);
        }
        
        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }
        
        public async Task<User> CreateUser(User user)
        {
            
            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(long id, User user)
        {
            var oldUser = await _context.Users.FindAsync(id);
            oldUser.Name = user.Name;
            oldUser.Voucher = user.Voucher;
            oldUser.RoleId = user.RoleId;
            oldUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return user;
        }
        public async Task DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserByName(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}