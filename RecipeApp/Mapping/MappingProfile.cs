using System.Collections.Generic;
using AutoMapper;
using RecipeApp.Dtos;
using RecipeApp.Dtos.AccountDtos;
using RecipeApp.Dtos.CategoryDtos;
using RecipeApp.Dtos.RecipeDtos;
using RecipeApp.Models;

namespace RecipeApp.IMapper
{
    public class MappingProfile : Profile
    {
        /**
         * A MappingProfile to determine mapping options.
         */
        public MappingProfile()
        {
            CreateMap<AccountModel, AccountDto>()
            // AccountModel -> AccountDto
            .ReverseMap()
            // AccountDto -> AccountModel
            .PreserveReferences();

            CreateMap<AccountModel, AddUserDto>()
            // AccountModel -> AddUserDto
            .ReverseMap()
            // AddUserDto -> AccountModel
            .PreserveReferences();

            CreateMap<AccountModel, UpdateUserDto>()
            // AccountModel -> UpdateUserDto
            .ReverseMap()
            // UpdateUserDto -> AccountModel
            .PreserveReferences();

            CreateMap<AccountModel, AccountListDto>()
            // AccountModel -> AccountListDto
            .ReverseMap()
            // AccountListDto -> AccountModel
            .PreserveReferences();

            CreateMap<RoleModel, RoleListDto>()
            // RoleModel -> RoleListDto
            .ReverseMap()
            // RoleListDto -> RoleModel
            .PreserveReferences();

            CreateMap<RoleModel, AddRoleDto>()
            // RoleModel -> AddRoleDto
            .ReverseMap()
            // AddRoleDto -> RoleModel
            .PreserveReferences();

            CreateMap<RecipeModel, RecipeListDto>()
            // RecipeModel -> RecipeListDto
            .ReverseMap()
            // RecipeListDto -> RecipeModel
            .PreserveReferences();

            CreateMap<RecipeModel, AddRecipeDto>()
            // RecipeModel -> AddRecipeDto
            .ReverseMap()
            // AddRecipeDto -> RecipeModel
            .PreserveReferences();

            CreateMap<RecipeModel, UpdateRecipeDto>()
            // RecipeModel -> UpdateRecipeDto
            .ReverseMap()
            // UpdateRecipeDto -> RecipeModel
            .PreserveReferences();

            CreateMap<RecipeModel, RecipeDto>()
            // RecipeModel -> RecipeDto
            .ReverseMap()
            // RecipeDto -> RecipeModel
            .PreserveReferences();

            CreateMap<CategoryModel, CategoryListDto>()
            // CategoryModel -> CategoryListDto
            .ReverseMap()
            // CategoryListDto -> CategoryModel
            .PreserveReferences();

            CreateMap<CategoryModel, AddCategoryDto>()
            // CategoryModel -> AddCategoryDto
            .ReverseMap()
            // AddCategoryDto -> CategoryModel
            .PreserveReferences();

            CreateMap<CategoryModel, UpdateCategoryDto>()
            // CategoryModel -> UpdateCategoryDto
            .ReverseMap()
            // UpdateCategoryDto -> CategoryModel
            .PreserveReferences();
        }
    }
}
