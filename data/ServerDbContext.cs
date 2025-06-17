using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserApi.Services.Interfaces;
using UserApi.Services;
using RolesApi.Services.Interfaces;
using MenuApi.Services.Interfaces;
using server.Models;



namespace Server.Data
{
    public class ServerDbContext(DbContextOptions<ServerDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SaleCustomer> SaleCustomers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
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

            // modelBuilder.Entity<SalesOrderHeader>().HasNoKey().ToView(null);
            // modelBuilder.Entity<SalesOrderDetail>().HasNoKey().ToView(null);
            // modelBuilder.Entity<SaleCustomer>().HasNoKey().ToView(null);
            // modelBuilder.Entity<Person>().HasNoKey().ToView(null);

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

            modelBuilder.Entity<Address>()
        .HasMany(a => a.SalesOrderHeaderBillToAddresses) // Address has many SalesOrderHeader (as bill-to address)
        .WithOne(s => s.BillToAddress) // SalesOrderHeader has one BillToAddress
        .HasForeignKey(s => s.BillToAddressId);

        //  modelBuilder.Entity<BillOfMaterial>()
        // .HasOne(bom => bom.Component) // A BillOfMaterial has one Component (Product)
        // .WithMany(/* specify the inverse navigation property in Product, if one exists */)
        // .HasForeignKey(bom => bom.ComponentId);
  modelBuilder.Entity<Employee>()
                .HasOne(e => e.SalesPerson) // Employee has one SalesPerson
                .WithOne(s => s.BusinessEntity) // SalesPerson has one BusinessEntity (Employee)
                .HasForeignKey<SalesPerson>(s => s.BusinessEntityId); 

                modelBuilder.Entity<Person>()
        .HasOne(p => p.Employee)  // A Person has one Employee
        .WithOne(e => e.BusinessEntity) // An Employee has one BusinessEntity (Person)
        .HasForeignKey<Employee>(e => e.BusinessEntityId); // Employee is dependent, BusinessEntityId is the FK
 modelBuilder.Entity<Person>()
        .HasOne(p => p.Password)  // A Person has one Password
        .WithOne(pw => pw.BusinessEntity) // A Password has one BusinessEntity (Person)
        .HasForeignKey<Password>(pw => pw.BusinessEntityId); 

        modelBuilder.Entity<BillOfMaterial>()
    .HasNoKey();
    modelBuilder.Entity<BusinessEntityAddress>()
    .HasNoKey();
    modelBuilder.Entity<BusinessEntityContact>()
    .HasNoKey();
    modelBuilder.Entity<CountryRegion>()
    .HasNoKey();
    modelBuilder.Entity<Employee>()
    .HasKey(e => e.BusinessEntityId); 
    modelBuilder.Entity<EmployeeDepartmentHistory>()
    .HasNoKey();
    modelBuilder.Entity<EmployeePayHistory>()
    .HasNoKey();
    modelBuilder.Entity<Password>()
    .HasKey(pw => pw.BusinessEntityId);
    modelBuilder.Entity<Person>()
    .HasKey(p => p.BusinessEntityId); 
    modelBuilder.Entity<PersonCreditCard>()
    .HasNoKey();
    modelBuilder.Entity<PersonPhone>()
    .HasNoKey();
    modelBuilder.Entity<ProductInventory>()
    .HasNoKey();
    modelBuilder.Entity<ProductListPriceHistory>()
    .HasNoKey();
    modelBuilder.Entity<ProductModelIllustration>()
    .HasNoKey();
    modelBuilder.Entity<ProductModelProductDescriptionCulture>()
    .HasNoKey();
    modelBuilder.Entity<ProductVendor>()
    .HasNoKey();
    modelBuilder.Entity<PurchaseOrderHeader>()
    .HasNoKey();
    modelBuilder.Entity<SaleCustomer>()
    .HasNoKey();
    modelBuilder.Entity<SalesOrderHeader>()
        .HasKey(soh => soh.SalesOrderId);
    modelBuilder.Entity<SalesOrderHeaderSalesReason>()
    .HasNoKey();
    modelBuilder.Entity<SalesPerson>()
        .HasKey(sp => sp.BusinessEntityId);
    modelBuilder.Entity<SalesPersonQuotaHistory>()
    .HasNoKey();
    modelBuilder.Entity<SalesTerritory>()
    .HasNoKey();
    modelBuilder.Entity<SalesTerritoryHistory>()
    .HasNoKey();
    modelBuilder.Entity<SpecialOfferProduct>()
    .HasNoKey();
    modelBuilder.Entity<Store>()
    .HasNoKey();
    modelBuilder.Entity<UnitMeasure>()
    .HasNoKey();
    modelBuilder.Entity<Vendor>()
    .HasNoKey();
    modelBuilder.Entity<WorkOrder>()
    .HasNoKey();
    modelBuilder.Entity<WorkOrderRouting>()
    .HasNoKey();
    

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