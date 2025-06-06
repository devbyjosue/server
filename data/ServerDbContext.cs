using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using UserApi.Services.Interfaces;
using UserApi.Services;
using RolesApi.Models;
using RolesApi.Services.Interfaces;

namespace Server.Data
{
    public class ServerDbContext(DbContextOptions<ServerDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdAt = new DateTime(2025, 6, 5, 12, 0, 0); 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Josue", Voucher = "v81137", RoleId=2, CreatedAt = createdAt, UpdatedAt = createdAt },
                new User { Id = 2, Name = "Ricardo", Voucher = "nosecual", RoleId=1, CreatedAt = createdAt, UpdatedAt = createdAt  },
                new User { Id = 3, Name = "Daniel", Voucher = "nosecual", RoleId=2,CreatedAt = createdAt, UpdatedAt = createdAt  }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", CreatedAt = createdAt, UpdatedAt = createdAt },
                new Role { Id = 2, Name = "User", CreatedAt = createdAt, UpdatedAt = createdAt }
            );
        }
    }
}