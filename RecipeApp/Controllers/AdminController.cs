using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApp.Dtos;
using RecipeApp.Dtos.AccountDtos;
using RecipeApp.Dtos.CategoryDtos;
using RecipeApp.Dtos.RecipeDtos;
using RecipeApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApp.Controllers
{
    /**
     * Controlling user and authenticate actions.
     */
    [Authorize(Policy = "Permission")]
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAccountService accountService;
        private readonly IRecipeService recipeService;
        public AdminController(ILogger<AdminController> logger, IAccountService accountService, IRecipeService recipeService)
        {
            _logger = logger;
            this.accountService = accountService;
            this.recipeService = recipeService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Recipes()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (Account != null)
                return RedirectToAction("index", "admin");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromForm] AccountDto accountDto)
        {
            //Incoming data transfer object mapped to the model(AccountModel) via AutoMapper
            //Mapper will be more useful as the application develops
            (ActionResult result, string token) = accountService.Authenticate(accountDto);

            if (token != null)
                setTokenCookie(token);


            return result;
        }

        [HttpGet]
        public async Task<ActionResult<AccountRoleListDto>> GetUsers()
        {
            AccountRoleListDto result = await accountService.GetUsersAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromForm] AddUserDto userDto)
        {
            ActionResult result = await accountService.AddUserAsync(userDto);

            return result;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm] UpdateUserDto userDto)
        {

            ActionResult result = await accountService.UpdateUserAsync(userDto);

            return result;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromQuery] string id)
        {
            ActionResult result = await accountService.DeleteUserAsync(id);

            return result;
        }

        [HttpGet]
        public async Task<List<RoleListDto>> GetRoles()
        {
            List<RoleListDto> roles = await accountService.GetRolesAsync();

            return roles;
        }

        [HttpPost]
        public async Task<ActionResult> AddRole([FromForm] AddRoleDto roleDto)
        {
            ActionResult result = await accountService.AddRoleAsync(roleDto);

            return result;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRole([FromForm] UpdateRoleDto roleDto)
        {
            ActionResult result = await accountService.UpdateRoleAsync(roleDto);

            return result;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRole([FromQuery] string id)
        {
            ActionResult result = await accountService.DeleteRoleAsync(id);

            return result;
        }

        [HttpGet]
        public async Task<List<CategoryListDto>> GetCategories()
        {
            List<CategoryListDto> roles = await recipeService.GetCategoriesAsync();

            return roles;
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromForm] AddCategoryDto categoryDto)
        {
            ActionResult result = await recipeService.AddCategoryAsync(categoryDto);

            return result;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory([FromForm] UpdateCategoryDto categoryDto)
        {
            ActionResult result = await recipeService.UpdateCategoryAsync(categoryDto);

            return result;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory([FromQuery] string id)
        {
            ActionResult result = await recipeService.DeleteCategoryAsync(id);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> AddRecipe([FromForm] AddRecipeDto recipeDto)
        {
            if (recipeDto.ImageFile != null)
            {
                recipeDto.ImagePath = Utility.UtilityMethods.UploadedFile(recipeDto.ImageFile);

            }
            ActionResult result = await recipeService.AddRecipeAsync(recipeDto);

            return result;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRecipe([FromForm] UpdateRecipeDto recipeDto)
        {
            if (recipeDto.ImageFile != null)
            {
                recipeDto.ImagePath = Utility.UtilityMethods.UploadedFile(recipeDto.ImageFile);

            }

            ActionResult result = await recipeService.UpdateRecipeAsync(recipeDto);

            return result;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe([FromQuery] string id)
        {
            ActionResult result = await recipeService.DeleteRecipeAsync(id);

            return result;
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            if (Account != null)
            {
                Response.Cookies.Delete("at");
            }

            return Ok();
        }

        private void setTokenCookie(string jwtToken)
        {
            var jwtCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
            };
            Response.Cookies.Append("at", jwtToken, jwtCookieOptions);
        }
    }
}
