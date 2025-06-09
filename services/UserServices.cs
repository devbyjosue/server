using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services.Interfaces;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly ServerDbContext _context;
        public UserServices(ServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUser(long id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateUser(User user)
        {
            var alreadyUser = await _context.Users.FirstOrDefaultAsync( x => x.Name == user.Name);
            if (alreadyUser != null)
            {
                throw new Exception("User already exists");
            }
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