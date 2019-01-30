using Microsoft.AspNetCore.Mvc;

namespace _25_MVC_AREAS.Areas.Area2.Controllers
{
    [Area("Area2")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
