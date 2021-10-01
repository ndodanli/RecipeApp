using System.Collections.Generic;

namespace RecipeApp.Dtos.ErrorDtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
        }

        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
        public int Status { get; set; }
    }
}
