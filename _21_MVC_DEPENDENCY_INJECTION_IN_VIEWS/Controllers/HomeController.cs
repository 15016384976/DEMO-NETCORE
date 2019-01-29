using Microsoft.AspNetCore.Mvc;

namespace _21_MVC_DEPENDENCY_INJECTION_IN_VIEWS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
