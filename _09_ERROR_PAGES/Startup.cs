using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _09_ERROR_PAGES
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.UseExceptionHandler(builder =>
                {
                    builder.Run(async httpContext =>
                    {
                        await httpContext.Response.WriteAsync(httpContext.Features.Get<IExceptionHandlerFeature>().Error.Message);
                    });
                });
            }
        }
    }
}
