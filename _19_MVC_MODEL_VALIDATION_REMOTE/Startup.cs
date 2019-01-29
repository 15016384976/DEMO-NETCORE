using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace _19_MVC_MODEL_VALIDATION_REMOTE
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }

    public class EmployeeInputModel
    {
        [Required]
        [Remote(action: "ValidateEmployeeNo", controller: "Home")]
        public string EmployeeNo { get; set; }
    }
}
