using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace RolesApi.Services.Interfaces
{
    public interface IRolesServices
    {
        Task<List<Role>> GetRoles();
        Task<Role> GetRole(long id);
        Task<Role> GetRoleByName(string name);
        Task<Role> CreateRole(Role role);
        Task<Role> UpdateRole(long Id,Role role);
        Task DeleteRole(long id);
        Task DeleteRoleByName(string name);
    }
}