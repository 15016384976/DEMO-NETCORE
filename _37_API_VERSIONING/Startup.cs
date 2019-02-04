using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace _37_API_VERSIONING
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(apiVersioningOptions =>
            {
                apiVersioningOptions.ReportApiVersions = true;
                apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
                apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader("api-version");

                apiVersioningOptions.Conventions.Controller<Controllers.WriterControllerV1>().HasApiVersion(new ApiVersion(1, 0));
                apiVersioningOptions.Conventions.Controller<Controllers.WriterControllerV2>().HasApiVersion(new ApiVersion(2, 0));
            });

            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseDeveloperExceptionPage();
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }
}
