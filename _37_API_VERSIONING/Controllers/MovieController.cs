using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    // /movie?api-version=1.0
    [ApiVersion("1.0")]
    [Route("movie")]
    public class MovieControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }
    }

    // /movie?api-version=2.0
    [ApiVersion("2.0")]
    [Route("movie")]
    public class MovieControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 2");
        }
    }
}
