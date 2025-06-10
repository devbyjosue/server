using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuApi.Models;
using MenuApi.Services.Interfaces;
using Server.Data;
using Microsoft.EntityFrameworkCore;


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
        // public async Task<Menu> GetMenu(long id)
        // {
        //     return await _context.Menus.FindAsync(id);
        // }
        public async Task<Menu> GetMenuByName(string name)
        {
            return await _context.Menus.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<object>> GetMenusWithRoles()
        {
            var menusWithRoles = await _context.Menus
                .Include(m => m.MenuRoles)
                .ThenInclude(mr => mr.Role)
                .Select(m => new 
                { 
                    m.Id, 
                    m.Name, 
                    Roles = (m.MenuRoles != null && m.MenuRoles.Any()) 
                ? m.MenuRoles.Select(mr => mr.Role.Name).ToList() 
                : new List<string>()
                })
                .ToListAsync();
            return menusWithRoles;
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