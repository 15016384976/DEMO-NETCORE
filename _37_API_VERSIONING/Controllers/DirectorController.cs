using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    // /director
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("directors")]
    public class DirectorController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }

        [HttpGet, MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            return Content("Version 2");
        }
    }
}
