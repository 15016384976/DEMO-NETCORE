using Microsoft.AspNetCore.Mvc;

namespace _22_MVC_LAYOUT_PAGES.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserViewModel
            {
                Firstname = "F",
                Surname = "S"
            };
            return View(model);
        }
    }
}
