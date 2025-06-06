using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RolesApi.Models;
using RolesApi.Services.Interfaces;
using Server.Data;
using Microsoft.EntityFrameworkCore;


namespace RolesApi.Services
{
    public class RolesServices : IRolesServices
    {
        private readonly ServerDbContext _context;
        public RolesServices(ServerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role> GetRole(long id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<Role> UpdateRole(long id, Role role)
        {
            var oldRole = await _context.Roles.FindAsync(id);
            oldRole.Name = role.Name;
            oldRole.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task DeleteRole(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRoleByName(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}