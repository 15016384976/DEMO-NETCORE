using Microsoft.AspNetCore.Mvc;

namespace _29_MVC_TAG_HELPER_COMPONENT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
