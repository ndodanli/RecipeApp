using AutoMapper;
using AutoMapper.Configuration;
using Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RecipeApp.Dtos.CategoryDtos;
using RecipeApp.Dtos.RecipeDtos;
using RecipeApp.Helpers;
using RecipeApp.IMapper;
using RecipeApp.Models;
using RecipeApp.Utility;
using RecipeApp.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using IConfigE = Microsoft.Extensions.Configuration;

namespace RecipeApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoCollection<RecipeModel> recipes;
        private readonly IMongoCollection<CategoryModel> categories;
        private readonly AutoMapper.IMapper mapper;
        private readonly string secretKey;
        private readonly RedisCacheManager redisManager;
        public RecipeService(IConfigE.IConfiguration configuration)
        {
            //Mapper configuration
            MapperConfigurationExpression configurationExpression = new MapperConfigurationExpression();
            configurationExpression.AddProfile(new MappingProfile());
            MapperConfiguration config = new MapperConfiguration(configurationExpression);
            mapper = config.CreateMapper();

            //Database configuration.
            MongoClient client = new MongoClient(configuration.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            recipes = database.GetCollection<RecipeModel>("Recipes");
            categories = database.GetCollection<CategoryModel>("Categories");
            secretKey = configuration.GetSection(nameof(AuthSettings)).GetSection("SecretKey").Value;

            redisManager = new RedisCacheManager();
        }

        /**
         * Get a recipe with slug.
         */
        public async Task<RecipeDto> GetRecipeAsync(string slug)
        {
            RecipeModel recipe = await recipes.Find(x => x.Slug == slug)
            .FirstOrDefaultAsync();

            RecipeDto recipeDto = mapper.Map<RecipeDto>(recipe);

            if (recipeDto != null)
                recipeDto.Category = await GetCategoryByIdAsync(recipe.CategoryId);

            return recipeDto;
        }

        /**
         * Get recipes without any filter.
         */
        public async Task<RecipeCategoryListDto> GetRecipesAsync()
        {
            var recipeCategory = redisManager.Get<RecipeCategoryListDto>(RedisKeys.RecipeCategoryKey);

            if (recipeCategory == null)
            {
                List<RecipeModel> recipeList = await recipes.Find(_ => true)
            .ToListAsync();

                List<RecipeListDto> recipeListedDto = new List<RecipeListDto>();

                recipeList.ForEach(x => recipeListedDto.Add(mapper.Map<RecipeListDto>(x)));

                foreach (RecipeListDto recipe in recipeListedDto)
                {
                    recipe.Category = await GetCategoryByIdAsync(recipe.CategoryId);
                }

                List<CategoryListDto> categoryList = await GetCategoriesAsync();

                List<string> categoryNames = new List<string>();

                foreach (CategoryListDto category in categoryList)
                {
                    categoryNames.Add(category.Name);
                }

                RecipeCategoryListDto result = new RecipeCategoryListDto()
                {
                    Recipes = recipeListedDto,
                    CategoryNames = categoryNames
                };

                redisManager.Set(RedisKeys.RecipeCategoryKey, result, 60);

                recipeCategory = redisManager.Get<RecipeCategoryListDto>(RedisKeys.RecipeCategoryKey);
            }

            return recipeCategory;
        }

        /**
         * Get recipes by filter(most viewed or last viewed).
         */
        public async Task<List<RecipeListDto>> GetRecipesByFilterAsync(string filter)
        {
            var recipelist = redisManager.Get<List<RecipeListDto>>(RedisKeys.RecipesKey);

            if (recipelist == null)
            {
                List<RecipeModel> recipeList;

                recipeList = await recipes.Find(_ => true)
                .ToListAsync();

                List<RecipeListDto> recipeListedDto = new List<RecipeListDto>();

                recipeList.ForEach(x => recipeListedDto.Add(mapper.Map<RecipeListDto>(x)));

                foreach (RecipeListDto recipe in recipeListedDto)
                {
                    recipe.Category = await GetCategoryByIdAsync(recipe.CategoryId);
                }

                redisManager.Set(RedisKeys.RecipesKey, recipeListedDto, 60);

                recipelist = redisManager.Get<List<RecipeListDto>>(RedisKeys.RecipesKey);
            }
            if (filter == "most")
            {
                recipelist.Sort((x, y) => y.View.CompareTo(x.View));
            }

            return recipelist;
        }

        /**
         * Add a recipe.
         */
        public async Task<ActionResult> AddRecipeAsync(AddRecipeDto recipeDto)
        {
            RecipeModel recipe = mapper.Map<RecipeModel>(recipeDto);

            recipe.CategoryId = await GetCategoryIdByNameAsync(recipeDto.CategoryName);

            await recipes.InsertOneAsync(recipe);

            redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

            return new OkResult();
        }

        /**
         * Update a recipe with id.
         */
        public async Task<ActionResult> UpdateRecipeAsync(UpdateRecipeDto recipeDto)
        {
            RecipeModel recipe = await recipes.Find(x => x.Id == recipeDto.Id)
            .FirstOrDefaultAsync();

            RecipeModel recipeModel = mapper.Map<RecipeModel>(recipeDto);

            if (recipe != null)
            {
                UtilityMethods.UpdateProps<RecipeModel>(recipe, recipeModel);

                if (recipeDto.CategoryName != null)
                {
                    recipe.CategoryId = await GetCategoryIdByNameAsync(recipeDto.CategoryName);
                }

                FilterDefinition<RecipeModel> filter = Builders<RecipeModel>.Filter.Eq(x => x.Id, recipeDto.Id);
                UpdateDefinition<RecipeModel> update = Builders<RecipeModel>.Update
                .Set(x => x.Name, recipe.Name)
                .Set(x => x.ImagePath, recipe.ImagePath)
                .Set(x => x.Slug, recipe.Slug)
                .Set(x => x.Servings, recipe.Servings)
                .Set(x => x.Directions, recipe.Directions)
                .Set(x => x.Ingredients, recipe.Ingredients)
                .Set(x => x.CategoryId, recipe.CategoryId);

                UpdateOptions options = new UpdateOptions { IsUpsert = true };

                await recipes.UpdateOneAsync(filter, update, options);

                redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

                return new OkResult();
            }

            return new BadRequestResult();
        }

        /**
         * Delete a recipe with id.
         */
        public async Task<ActionResult> DeleteRecipeAsync(string id)
        {
            await recipes.DeleteOneAsync(x => x.Id == id);

            redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

            return new OkResult();
        }

        /**
         * Get a category with name.
         */
        private async Task<string> GetCategoryIdByNameAsync(string categoryName)
        {
            CategoryModel category = await categories.Find(x => x.Name == categoryName)
                .FirstOrDefaultAsync();

            return category.Id;
        }

        /**
         * Get a category with id.
         */
        public async Task<string> GetCategoryByIdAsync(string id)
        {
            CategoryModel category = await categories.Find(x => x.Id == id).FirstOrDefaultAsync();

            return category?.Name;
        }

        /**
         * Get categories.
         */
        public async Task<List<CategoryListDto>> GetCategoriesAsync()
        {
            List<CategoryModel> categoryList = await categories.Find(_ => true)
            .ToListAsync();

            List<CategoryListDto> categoryListDto = new List<CategoryListDto>();

            categoryList.ForEach(x => categoryListDto.Add(mapper.Map<CategoryListDto>(x)));

            return categoryListDto;
        }

        /**
         * Delete a category with id.
         */
        public async Task<ActionResult> DeleteCategoryAsync(string id)
        {
            await categories.DeleteOneAsync(x => x.Id == id);

            redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

            return new OkResult();
        }

        /**
         * Update a category with id.
         */
        public async Task<ActionResult> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            CategoryModel role = await categories.Find(x => x.Id == categoryDto.Id)
            .FirstOrDefaultAsync();

            CategoryModel recipeModel = mapper.Map<CategoryModel>(categoryDto);

            if (role != null)
            {
                UtilityMethods.UpdateProps<CategoryModel>(role, recipeModel);
                FilterDefinition<CategoryModel> filter = Builders<CategoryModel>.Filter.Eq(x => x.Id, role.Id);
                UpdateDefinition<CategoryModel> update = Builders<CategoryModel>.Update.Set(x => x.Name, role.Name);

                UpdateOptions options = new UpdateOptions { IsUpsert = true };

                await categories.UpdateOneAsync(filter, update, options);

                redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

                return new OkResult();
            }

            return new BadRequestResult();
        }

        /**
         * Add a category.
         */
        public async Task<ActionResult> AddCategoryAsync(AddCategoryDto categoryDto)
        {
            CategoryModel category = mapper.Map<CategoryModel>(categoryDto);

            await categories.InsertOneAsync(category);

            redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);

            return new OkResult();
        }

        /**
         * Increase recipe's view count by 1 if the page is visited.
         */
        public async Task<ActionResult> IncreaseViewCount(string slug)
        {
            RecipeModel recipe = await recipes.Find(x => x.Slug == slug).FirstOrDefaultAsync();

            if (recipe != null)
            {
                FilterDefinition<RecipeModel> filter = Builders<RecipeModel>.Filter.Eq(x => x.Slug, slug);
                UpdateDefinition<RecipeModel> update = Builders<RecipeModel>.Update
                .Set(x => x.View, (recipe.View + 1));

                UpdateOptions options = new UpdateOptions { IsUpsert = true };

                await recipes.UpdateOneAsync(filter, update, options);

                redisManager.RemoveByPattern(RedisKeys.RecipeCategoryKey);
                redisManager.RemoveByPattern(RedisKeys.RecipesKey);

                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
