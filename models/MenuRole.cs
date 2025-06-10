using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuApi.Models;
using RolesApi.Models;

namespace MenuRoleApi.Models
{
    public class MenuRole
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public Menu Menu { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}