using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace _24_MVC_VIEW_COMPONENTS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressFormatter, AddressFormatter>();
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public interface IAddressFormatter
    {
        string Format(string line1, string line2, string line3);
    }

    public class AddressFormatter : IAddressFormatter
    {
        public string Format(string line1, string line2, string line3)
        {
            return $"{line1}, {line2}, {line3}";
        }
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }

    public class AddressViewModel
    {
        public int EmployeeId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string FormattedValue { get; set; }
    }

    [ViewComponent(Name = "Address")]
    public class AddressComponent : ViewComponent
    {
        private readonly IAddressFormatter _addressFormatter;

        public AddressComponent(IAddressFormatter addressFormatter)
        {
            _addressFormatter = addressFormatter;
        }

        public async Task<IViewComponentResult> InvokeAsync(int employeeId)
        {
            var model = new AddressViewModel
            {
                EmployeeId = employeeId,
                Line1 = "Secret Location",
                Line2 = "London",
                Line3 = "UK"
            };
            model.FormattedValue = _addressFormatter.Format(model.Line1, model.Line2, model.Line3);
            return View("Default", model);
        }
    }

    public class UserInfoViewModel
    {
        public string Username { get; set; }
        public string LastLogin { get; set; }
    }

    public class UserInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new UserInfoViewModel
            {
                Username = "james@bond.com",
                LastLogin = DateTime.Now.ToString()
            };
            return View("Index", model);
        }
    }
}
