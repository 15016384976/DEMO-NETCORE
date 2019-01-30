using Microsoft.AspNetCore.Mvc;

namespace _24_MVC_VIEW_COMPONENTS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new EmployeeViewModel
            {
                Id = 1,
                Firstname = "James",
                Surname = "Bond"
            };
            return View(model);
        }
    }
}
