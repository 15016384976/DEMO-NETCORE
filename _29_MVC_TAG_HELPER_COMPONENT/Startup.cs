using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace _29_MVC_TAG_HELPER_COMPONENT
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            // setup request pipeline using middleware
        }
    }
}
