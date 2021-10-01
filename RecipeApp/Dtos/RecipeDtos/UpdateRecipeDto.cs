using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.RecipeDtos
{
    public class UpdateRecipeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IFormFile ImageFile { get; set; }

        public string ImagePath { get; set; }

        public string Slug { get; set; }

        public int Servings { get; set; } = -1;

        public string Directions { get; set; }

        public string[] Ingredients { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }
    }
}
