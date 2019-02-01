using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _26_MVC_TAG_HELPERS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new EmployeeViewModel
            {
                Id = 5
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var json = JsonConvert.SerializeObject(model);
            return Content(json);
        }

        public IActionResult Echo(int id)
        {
            return Content(id.ToString());
        }

        public IActionResult NamedRoute(int id)
        {
            return Content(id.ToString());
        }
    }
}
