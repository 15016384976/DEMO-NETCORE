using Microsoft.AspNetCore.Mvc;

namespace _37_API_VERSIONING.Controllers
{
    // /actor/v1.0
    [ApiVersion("1.0")]
    [Route("actor/v{ver:apiVersion}")]
    public class ActorControllerV1 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 1");
        }
    }

    // /actor/v2.0
    [ApiVersion("2.0")]
    [Route("actor/v{ver:apiVersion}")]
    public class ActorControllerV2 : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Version 2");
        }
    }
}
