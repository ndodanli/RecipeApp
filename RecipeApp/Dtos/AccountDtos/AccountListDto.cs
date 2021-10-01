using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.AccountDtos
{
    public class AccountListDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string RoleId { get; set; }
    }
}
