using Microsoft.AspNetCore.Mvc;

namespace _24_MVC_VIEW_COMPONENTS.Controllers
{
    public class JQueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
