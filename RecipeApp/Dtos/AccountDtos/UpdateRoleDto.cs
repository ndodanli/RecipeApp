using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.AccountDtos
{
    public class UpdateRoleDto
    {
        public string RoleId { get; set; }
        public string[] Permissions { get; set; }
    }
}
