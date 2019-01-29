using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace _21_MVC_DEPENDENCY_INJECTION_IN_VIEWS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILookupService, LookupService>();
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }

    public interface ILookupService
    {
        List<SelectListItem> Genres { get; }
    }

    public class LookupService : ILookupService
    {
        public List<SelectListItem> Genres
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = "Thriller" },
                    new SelectListItem { Value = "1", Text = "Comedy" },
                    new SelectListItem { Value = "2", Text = "Drama" },
                    new SelectListItem { Value = "3", Text = "Romance" },
                };
            }
        }
    }
}
