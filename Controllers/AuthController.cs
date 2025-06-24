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
using System.Security.Claims;


namespace Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetAuthUser()
        {
            var UserId = HttpContext.User.Identity.Name;
            // string vBadge = UserId.Remove(0, 4);

            Console.WriteLine($"Authenticated user: {UserId}");

            return Ok(UserId);
        }
    }
}