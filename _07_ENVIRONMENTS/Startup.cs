using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _07_ENVIRONMENTS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            var environmentName = hostingEnvironment.EnvironmentName;

            if (hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.Run(async httpContext =>
                {
                    await httpContext.Response.WriteAsync($"EnvironmentName is {environmentName}");
                });
            }
        }
    }
}
