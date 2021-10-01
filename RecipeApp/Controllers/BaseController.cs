using Microsoft.AspNetCore.Mvc;
using RecipeApp.Dtos;

namespace RecipeApp.Controllers
{
    /**
     * A base controller so the other controllers that needs user
     * access the authenticated user.
     */
    [Controller]
    public abstract class BaseController : Controller
    {
        public AccountDto Account => (AccountDto)HttpContext.Items["Account"];
    }
}
