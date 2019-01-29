using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _15_MVC_ROUTING.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlHelper _urlHelper;

        public UrlController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IActionResult Index()
        {
            var links = new List<string>();
            links.Add(_urlHelper.RouteUrl("goto_one", new { }));
            links.Add(_urlHelper.Action("PageOne", "Home", new { }));
            links.Add(_urlHelper.Link("goto_one", new { }));
            return Json(links);
        }
    }
}
