using Microsoft.AspNetCore.Mvc;

namespace _23_MVC_PARTIAL_VIEWS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new EmployeeViewModel
            {
                Id = 1,
                Firstname = "James",
                Surname = "Bond",
                Address = new AddressViewModel
                {
                    Line1 = "Secret Location",
                    Line2 = "London",
                    Line3 = "UK"
                }
            };
            return View(model);
        }
    }
}
