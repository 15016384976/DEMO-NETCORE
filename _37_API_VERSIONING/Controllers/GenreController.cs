using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("genre")]
    public class GenresControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }
    }

    [ApiVersion("2.0")]
    [Route("genre")]
    public class GenresControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 2");
        }
    }
}
