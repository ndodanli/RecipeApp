
namespace RecipeApp.Dtos.RecipeDtos
{
    public class RecipeDto
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Category { get; set; }

        public int Servings { get; set; } = -1;

        public string Directions { get; set; }

        public string[] Ingredients { get; set; }
    }
}
