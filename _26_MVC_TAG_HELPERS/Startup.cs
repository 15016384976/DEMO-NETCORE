using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _26_MVC_TAG_HELPERS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "namedRoute", template: "call-named-route/{id?}", defaults: new { controller = "Home", action = "NamedRoute" });
                routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class EmployeeViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string BlogUrl { get; set; }
        [Phone]
        public string MobileNo { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
        [Display(Name = "Part Time?")]
        public bool IsPartTime { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Interests { get; set; }
        public Gender Gender { get; set; }
        public List<SelectListItem> GetTitles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Value= "Mr", Text = "Mr."},
                new SelectListItem{ Value= "Mrs", Text = "Mrs."},
                new SelectListItem{ Value= "Dr", Text = "Dr."},
            };
        }
        public List<SelectListItem> GetInterests()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Value= "C", Text = "Coding"},
                new SelectListItem{ Value= "G", Text = "Gaming"},
                new SelectListItem{ Value= "M", Text = "Watching Movies"},
                new SelectListItem{ Value= "S", Text = "Sleeping"},
            };
        }
    }
}
