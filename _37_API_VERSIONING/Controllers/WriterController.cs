using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    [Route("writers")]
    public class WriterControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }
    }

    [Route("writers")]
    public class WriterControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 2");
        }
    }
}
