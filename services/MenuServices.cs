using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuApi.Models;
using MenuApi.Services.Interfaces;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using MenuRoleApi.Models;
using System.Text.Json;


namespace MenuApi.Services
{
    public class MenuServices : IMenuServices
    {
        private readonly ServerDbContext _context;
        public MenuServices(ServerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Menu>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }
       
        public async Task<Menu> GetMenuByName(string name)
        {
            return await _context.Menus.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<object>> GetMenusWithRoles()
        {
            // var firstMenuRoles = new MenuRole
            // {
            //     MenuId = 1,
            //     RoleId = 3,
            //     canView = false,
            //     canEdit = false
            // };
            // _context.MenuRoles.Add(firstMenuRoles);
            // await _context.SaveChangesAsync();
            

            var menusWithRoles = await _context.MenuRoles
                .Select(mr => new 
                {
                    mr.Id,
                    mr.MenuId,
                    MenuName = mr.Menu.Name,
                    RoleName = mr.Role.Name,
                    RoleId = mr.RoleId,
                    mr.canView,
                    mr.canEdit
                }).ToListAsync();

      
            return menusWithRoles;
        }
        public async Task<object> UpdateMenusWithRoles(long id, bool canView, bool canEdit)
        {

            var MenuRoles = await _context.MenuRoles.Where(mr => mr.Id == id).ToListAsync();

            Console.WriteLine("----------------------------------------------------");
            foreach (var menuRole in MenuRoles)
            {
                Console.WriteLine($"Id {menuRole.Id}");
                Console.WriteLine($"Menu {menuRole.MenuId}");
                Console.WriteLine($"Role {menuRole.RoleId}");
                Console.WriteLine($"canView {menuRole.canView} {canView}");
                Console.WriteLine($"canEdit {menuRole.canEdit} {canEdit}");

                menuRole.canView = canView;
                menuRole.canEdit = canEdit;
            }
            await _context.SaveChangesAsync();
            return MenuRoles;
        }


        public async Task<Menu> CreateMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }
        public async Task<Menu> UpdateMenu(long id, Menu menu)
        {
            var oldMenu = await _context.Menus.FindAsync(id);
            oldMenu.Name = menu.Name;
            oldMenu.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return menu;
        }
        public async Task DeleteMenu(long id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }


        public async Task<Menu> DeleteMenuByName(string name)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Name == name);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
            return menu;
        }
    }
}