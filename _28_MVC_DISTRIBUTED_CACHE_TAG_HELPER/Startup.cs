using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace _28_MVC_DISTRIBUTED_CACHE_TAG_HELPER
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            /*
            serviceCollection.AddDistributedRedisCache(options =>
            {
                options.Configuration = "...";
            });
            */

            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }
}
