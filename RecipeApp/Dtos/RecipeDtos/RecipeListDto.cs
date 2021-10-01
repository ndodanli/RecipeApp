using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Dtos.RecipeDtos
{
    public class RecipeListDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Slug { get; set; }

        public int View { get; set; }

        public string Directions { get; set; }

        public int Servings { get; set; } = -1;

        public string[] Ingredients { get; set; }

        public string Category { get; set; }
        
        public string CategoryId { get; set; }
    }
}
