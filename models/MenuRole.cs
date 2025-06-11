using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuApi.Models;
using RolesApi.Models;
using System.Text.Json.Serialization;


namespace MenuRoleApi.Models
{
    public class MenuRole
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        
        [JsonIgnore]
        public Menu Menu { get; set; }

        public long RoleId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
    }
}