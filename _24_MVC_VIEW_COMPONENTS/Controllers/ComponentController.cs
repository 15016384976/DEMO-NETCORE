using Microsoft.AspNetCore.Mvc;

namespace _24_MVC_VIEW_COMPONENTS.Controllers
{
    public class ComponentController : Controller
    {
        public IActionResult UserInfo()
        {
            return ViewComponent("UserInfo"); // works: this component's view is in Views/Shared
        }

        public IActionResult Address()
        {
            return ViewComponent("Address", new { employeeId = 5 }); // doesn't works: this component's view is NOT in Views/
        }
    }
}

// UserInfoViewComponent || AddressViewComponent
// Views/[controller]/Components/[component]/[view].cshtml
// Views/Shared/Components/[component]/[view].cshtml