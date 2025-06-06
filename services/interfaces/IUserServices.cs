using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace UserApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(long id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(long Id,User user);
        Task DeleteUser(long id);
        Task DeleteUserByName(string name);
    }
}