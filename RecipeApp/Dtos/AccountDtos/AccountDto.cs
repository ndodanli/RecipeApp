using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Dtos
{
    public class AccountDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}