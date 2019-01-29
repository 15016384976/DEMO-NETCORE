using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace _15_MVC_ROUTING
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            serviceCollection.AddScoped<IUrlHelper>(serviceProvider =>
            {
                return new UrlHelper(serviceProvider.GetService<IActionContextAccessor>().ActionContext);
            });

            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(configureRoutes =>
            {
                configureRoutes.MapRoute(name: "goto_one", template: "one", defaults: new { controller = "Home", action = "PageOne" });
                configureRoutes.MapRoute(name: "goto_two", template: "two/{id?}", defaults: new { controller = "Home", action = "PageTwo" });
                configureRoutes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
