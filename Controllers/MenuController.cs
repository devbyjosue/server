using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MenuApi.Models;
using MenuApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using MenuApi.Services;

namespace MenuApi.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _menuServices;

        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetMenus()
        {
            var menus = await _menuServices.GetMenus();
            return Ok(menus);
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Menu>> GetMenu(long id)
        // {
        //     var menu = await _menuServices.GetMenu(id);
        //     return Ok(menu);
        // }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Menu>> GetMenuByName(string name)
        {
            var menu = await _menuServices.GetMenuByName(name);
            return Ok(menu);
        }
        [HttpGet("menu-roles")]
        public async Task<ActionResult<IEnumerable<object>>> GetMenusWithRoles()
        {
            var menusWithRoles = await _menuServices.GetMenusWithRoles();
            return Ok(menusWithRoles);
        }

        public class RoleUpdateRequest
        {
            public int id { get; set; }
            public Boolean canView { get; set; }
            public Boolean canEdit { get; set; }
        }

        [HttpPut("menu-roles/{id}")]
        public async Task<ActionResult<object>> UpdateMenusWithRoles(long id, RoleUpdateRequest request)
        {
            var updatedMenu = await _menuServices.UpdateMenusWithRoles(request.id, request.canView, request.canEdit);
            return updatedMenu == null ? NotFound(new { error = "Menu not found" }) : Ok(updatedMenu);

        }

        [HttpPost]
        public async Task<ActionResult<Menu>> CreateMenu([FromBody] Menu menu)
        {
            var menuDTO = new Menu
            {
                Name = menu.Name,
                CreatedAt = menu.CreatedAt,
                UpdatedAt = menu.UpdatedAt
            };

            var menus = await _menuServices.GetMenus();

            if (menus.Any(x => x.Name == menu.Name))
            {
                return BadRequest(new { error = "Menu already exists" });
            }

            var newMenu = await _menuServices.CreateMenu(menuDTO);
            return Ok(newMenu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Menu>> UpdateMenu(long id, [FromBody] Menu menu)
        {
            var updatedMenu = await _menuServices.UpdateMenu(id, menu);
            return Ok(updatedMenu);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(long id)
        {
            await _menuServices.DeleteMenu(id);
            return Ok();
        }
        [HttpDelete("name/{name}")]
        public async Task<ActionResult> DeleteMenuByName(string name)
        {
            await _menuServices.DeleteMenuByName(name);
            return Ok();
        }

    }
}