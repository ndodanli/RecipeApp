using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.AccountDtos
{
    public class AddRoleDto
    {
        public string Name { get; set; }
        public string[] Permissions { get; set; }
    }
}
