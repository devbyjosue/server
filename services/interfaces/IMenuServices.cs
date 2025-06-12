using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuApi.Models;

namespace MenuApi.Services.Interfaces
{
    public interface IMenuServices
    {
        Task<List<Menu>> GetMenus();
        // Task<Menu> GetMenu(long id);
        Task<Menu> GetMenuByName(string name);
        
        Task<IEnumerable<object>> GetMenusWithRoles();
        Task<object> UpdateMenusWithRoles(long id, bool canView, bool canEdit);

        Task<Menu> CreateMenu(Menu menu);
        Task<Menu> UpdateMenu(long id, Menu menu);
        Task DeleteMenu(long id);
        Task<Menu> DeleteMenuByName(string name);
    }
}