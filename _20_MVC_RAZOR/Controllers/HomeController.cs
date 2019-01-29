using Microsoft.AspNetCore.Mvc;

namespace _20_MVC_RAZOR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutMe()
        {
            var model = new AboutViewModel
            {
                Firstname = "F",
                Surname = "S"
            };
            return View("Bio", model);
        }
    }
}
