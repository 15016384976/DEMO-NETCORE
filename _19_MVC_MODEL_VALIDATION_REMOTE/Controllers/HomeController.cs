using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _19_MVC_MODEL_VALIDATION_REMOTE.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(EmployeeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var json = JsonConvert.SerializeObject(model);
            return Content(json);
        }

        public IActionResult ValidateEmployeeNo(string employeeNo)
        {
            if (employeeNo == "007")
                return Json(data: "007 is already assigned to James Bond!");
            return Json(data: true);
        }
    }
}
