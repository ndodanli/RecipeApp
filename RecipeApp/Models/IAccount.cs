namespace RecipeApp.Models
{
    public interface IAccount
    {
        string Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string RoleId { get; set; }
    }
}