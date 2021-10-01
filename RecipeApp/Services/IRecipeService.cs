using Microsoft.AspNetCore.Mvc;
using RecipeApp.Dtos.CategoryDtos;
using RecipeApp.Dtos.RecipeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public interface IRecipeService
    {
        Task<RecipeCategoryListDto> GetRecipesAsync();

        Task<ActionResult> AddRecipeAsync(AddRecipeDto recipeDto);
        Task<ActionResult> UpdateRecipeAsync(UpdateRecipeDto recipeDto);
        Task<ActionResult> DeleteRecipeAsync(string id);

        Task<List<CategoryListDto>> GetCategoriesAsync();

        Task<ActionResult> DeleteCategoryAsync(string id);

        Task<ActionResult> UpdateCategoryAsync(UpdateCategoryDto roleDto);

        Task<ActionResult> AddCategoryAsync(AddCategoryDto roleDto);

        Task<RecipeDto> GetRecipeAsync(string slug);

        Task<List<RecipeListDto>> GetRecipesByFilterAsync(string mostViewed);
        Task<ActionResult> IncreaseViewCount(string slug);
    }
}
