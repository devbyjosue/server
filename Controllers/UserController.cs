using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using UserApi.Services;
using Microsoft.AspNetCore.Cors;
using UserApi.Dto;


namespace UserApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userServices.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _userServices.GetUser(id);
            return Ok(user);
        }

        [HttpGet("userName/{name}")]
        public async Task<ActionResult<User>> GetUserByName(string name)
        {
            var user = await _userServices.GetUserByName(name);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser( User user)
        {
            var userDTO = new User
            {
                Name = user.Name,
                Voucher = user.Voucher,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            var users = await _userServices.GetUsers();

            if (users.Any(x => x.Name == user.Name))
            {
                return BadRequest(new { error = "User already exists"});
            }

           
            var newUser = await _userServices.CreateUser(userDTO);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(long id, [FromBody] User user)
        {
            var updatedUser = await _userServices.UpdateUser(id, user);
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            await _userServices.DeleteUser(id);
            return Ok();
        }
        [HttpDelete("name/{name}")]
        public async Task<ActionResult> DeleteUserByName(string name)
        {
            await _userServices.DeleteUserByName(name);
            return Ok();
        }
    }
}