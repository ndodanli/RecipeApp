using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RecipeApp.Dtos.AccountDtos
{
    public class AccountRoleListDto 
    {
        public List<AccountListDto> Users { get; set; }
        public List<string> Roles { get; set; }
    }
}
