using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RolesApi.Models;
using RolesApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using RolesApi.Services;
using Microsoft.AspNetCore.Cors;

namespace RolesApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _rolesServices;
        public RolesController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            var roles = await _rolesServices.GetRoles();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(long id)
        {
            var role = await _rolesServices.GetRole(id);
            return Ok(role);
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Role>> GetRoleByName(string name)
        {
            var role = await _rolesServices.GetRoleByName(name);
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            var newRole = await _rolesServices.CreateRole(role);
            return Ok(newRole);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(long id, [FromBody] Role role)
        {
            var updatedRole = await _rolesServices.UpdateRole(id, role);
            return Ok(updatedRole);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(long id)
        {
            await _rolesServices.DeleteRole(id);
            return Ok();
        }
        [HttpDelete("name/{name}")]
        public async Task<ActionResult> DeleteRoleByName(string name)
        {
            await _rolesServices.DeleteRoleByName(name);
            return Ok();
        }
    }
}