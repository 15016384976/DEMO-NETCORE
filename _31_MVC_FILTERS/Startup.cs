using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace _31_MVC_FILTERS
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
