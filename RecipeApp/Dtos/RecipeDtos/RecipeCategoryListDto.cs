using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.RecipeDtos
{
    public class RecipeCategoryListDto
    {
        public List<RecipeListDto> Recipes { get; set; }

        public List<string> CategoryNames { get; set; }
    }
}
