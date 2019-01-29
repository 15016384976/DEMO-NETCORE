using Microsoft.AspNetCore.Mvc;

namespace _15_MVC_ROUTING.Controllers
{
    [Route("work")]
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return Content("Work/Index");
        }

        [Route("one")]
        public IActionResult PageOne()
        {
            return Content("Work/One");
        }

        [HttpGet("two")]
        public IActionResult PageTwo()
        {
            return Content("(GET) Work/Two");
        }

        [HttpPost("two/{id?}")]
        public IActionResult PageTwo(int id)
        {
            return Content($"(POST) Work/Two: {id}");
        }
    }
}
