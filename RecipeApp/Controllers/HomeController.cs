using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApp.Dtos.RecipeDtos;
using RecipeApp.Models;
using RecipeApp.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RecipeApp.Controllers
{
    /**
     * Provides recipes and handling 404 pages.
     */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService recipeService;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            this.recipeService = recipeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("recipe/{slug}")]
        public IActionResult Recipe()
        {
            return View();
        }

        /**
         * If user wants the most viewed order, sending the recipe list in most viewed order, if doesn't,
         * sending "last" sending the recipe list in last viewed order. If filter is null, 
         * it will provide the "last" ordered recipe list and the categories.
         */
        [HttpGet]
        public async Task<ActionResult<RecipeListDto>> GetRecipes([FromQuery] string filter)
        {
            if (filter == "most")
            {
                List<RecipeListDto> recipeList = await recipeService.GetRecipesByFilterAsync(filter);

                return Ok(recipeList);
            }
            else if(filter == "last")
            {
                List<RecipeListDto> recipeList = await recipeService.GetRecipesByFilterAsync(filter);

                return Ok(recipeList);
            }
            else
            {
                RecipeCategoryListDto recipeList = await recipeService.GetRecipesAsync();

                return Ok(recipeList);
            }
        }

        /**
         * Providing the recipe by slug.
         */
        [HttpGet]
        public async Task<ActionResult<RecipeDto>> GetRecipe(string slug)
        {
            RecipeDto recipe = await recipeService.GetRecipeAsync(slug);

            await recipeService.IncreaseViewCount(slug);

            return Ok(recipe);
        }

        /**
         * Error page handling(origin/xxx, if there is no method to meet xxx, it will redirect to the error page).
         */
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
