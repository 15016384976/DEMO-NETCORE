using Microsoft.AspNetCore.Mvc;

namespace _15_MVC_ROUTING.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home/Index");
        }

        public IActionResult PageOne()
        {
            return Content("Home/One");
        }

        [HttpGet]
        public IActionResult PageTwo()
        {
            return Content("(GET) Home/Two");
        }

        [HttpPost]
        public IActionResult PageTwo(int id)
        {
            return Content($"(POST) Home/Two: {id}");
        }
    }
}
