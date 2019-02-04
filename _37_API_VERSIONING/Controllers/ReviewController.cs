using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    // /review [Headers -> api-version -> 1]
    [ApiVersion("1.0")]
    [Route("review")]
    public class ReviewControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }
    }

    // /review [Headers -> api-version -> 2]
    [ApiVersion("2.0")]
    [Route("review")]
    public class ReviewControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 2");
        }
    }
}
