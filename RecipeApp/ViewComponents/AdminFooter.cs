using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.ViewComponents
{
    public class AdminFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
