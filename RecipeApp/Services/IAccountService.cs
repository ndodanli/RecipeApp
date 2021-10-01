using Microsoft.AspNetCore.Mvc;
using RecipeApp.Dtos;
using RecipeApp.Dtos.AccountDtos;
using RecipeApp.Dtos.CategoryDtos;
using RecipeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public interface IAccountService
    {
        (ActionResult, string) Authenticate(AccountDto account);

        RoleModel GetRole(string roleId);

        Task<AccountRoleListDto> GetUsersAsync();

        Task<ActionResult> DeleteUserAsync(string id);

        Task<ActionResult> UpdateUserAsync(UpdateUserDto roleDto);

        Task<ActionResult> AddUserAsync(AddUserDto roleDto);

        Task<List<RoleListDto>> GetRolesAsync();

        Task<ActionResult> DeleteRoleAsync(string id);

        Task<ActionResult> UpdateRoleAsync(UpdateRoleDto roleDto);

        Task<ActionResult> AddRoleAsync(AddRoleDto roleDto);

    }
}