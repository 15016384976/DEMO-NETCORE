using Microsoft.AspNetCore.Mvc;

namespace _18_MVC_MODEL_VALIDATION.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello Model Validation");
        }

        [HttpPost]
        public IActionResult Save(EmployeeInputModel model)
        {
            if (model.Id == 1)
                ModelState.AddModelError("Id", "Id already exist");

            if (ModelState.IsValid)
                return Ok(model);

            return BadRequest(ModelState);
        }
    }
}
