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
using SalesApi.Dto;
using MenuApi.Models;
using MenuApi.Services.Interfaces;
using MenuRoleApi.Models;


namespace Server.Data
{
    public class ServerDbContext(DbContextOptions<ServerDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders => Set<SalesOrderHeader>();
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdAt = new DateTime(2025, 6, 5, 12, 0, 0); 
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().HasData(
            //     new User { Id = 1, Name = "Josue", Voucher = "v81137", RoleId=2, CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new User { Id = 2, Name = "Ricardo", Voucher = "nosecual", RoleId=1, CreatedAt = createdAt, UpdatedAt = createdAt  },
            //     new User { Id = 3, Name = "Daniel", Voucher = "nosecual", RoleId=2,CreatedAt = createdAt, UpdatedAt = createdAt  }
            // );

            // modelBuilder.Entity<Role>().HasData(
            //     new Role { Id = 1, Name = "Admin", CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new Role { Id = 2, Name = "User", CreatedAt = createdAt, UpdatedAt = createdAt }
            // );

            modelBuilder.Entity<SalesOrderHeader>().HasNoKey().ToView(null);

            // modelBuilder.Entity<RoleMenu>().HasData(
            //     new RoleMenu { Id = 1, RoleId = 1, MenuId = 1, CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new RoleMenu { Id = 2, RoleId = 1, MenuId = 2, CreatedAt = createdAt, UpdatedAt = createdAt },
            // )

            // modelBuilder.Entity<Menu>().HasData(
            //     new Menu { Id = 1, Name = "Roles", CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new Menu { Id = 2, Name = "Admin", CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new Menu { Id = 3, Name = "Home", CreatedAt = createdAt, UpdatedAt = createdAt },
            //     new Menu { Id = 4, Name = "Sales", CreatedAt = createdAt, UpdatedAt = createdAt }
            // );

            modelBuilder.Entity<MenuRole>()
                .HasOne(mr => mr.Menu)
                .WithMany(m => m.MenuRoles)
                .HasForeignKey(mr => mr.MenuId);

            modelBuilder.Entity<MenuRole>()
                .HasOne(mr => mr.Role)
                .WithMany(r => r.MenuRoles)
                .HasForeignKey(mr => mr.RoleId);
        }
    }
}