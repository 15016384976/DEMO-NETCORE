using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace _31_MVC_FILTERS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGreetingService, GreetingService>();

            serviceCollection.AddScoped<GreetingServiceFilter>();

            serviceCollection.AddMvc(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AddDeveloperResultFilter("Tahir Naushad")); // instance
                mvcOptions.Filters.Add(typeof(GreetDeveloperResultFilter)); // type
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public interface IGreetingService
    {
        string Greet(string name);
    }

    public class GreetingService : IGreetingService
    {
        public string Greet(string name)
        {
            return $"Hello {name}";
        }
    }

    public class HelloActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
    }

    public class HelloActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // runs before action method
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // runs after action method
        }
    }

    public class HelloAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // runs before action method
            await next();
            // runs after action method
        }
    }

    public class SkipActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new ContentResult
            {
                Content = "I'll skip the action execution"
            };
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Will not reach here
        }
    }

    public class ParseParameterActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            object param;
            if (context.ActionArguments.TryGetValue("param", out param))
                context.ActionArguments["param"] = param.ToString().ToUpper();
            else
                context.ActionArguments.Add("param", "I come from action filter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class AddDeveloperResultFilter : IResultFilter
    {
        private readonly string developer;

        public AddDeveloperResultFilter(string developer)
        {
            this.developer = developer;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(
                "Developer", new StringValues(this.developer));
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }

    public class GreetDeveloperResultFilter : IResultFilter
    {
        private readonly IGreetingService greetingService;

        public GreetDeveloperResultFilter(IGreetingService greetingService)
        {
            this.greetingService = greetingService;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Developer-Msg", new StringValues(this.greetingService.Greet("Tahir")));
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }

    public class HelloResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // runs before result execution
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // runs after result execution
        }
    }

    public class HelloAsyncResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            // runs before result execution
            await next();
            // runs after result execution
        }
    }

    public class SkipResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.Cancel = true;
            context.HttpContext.Response.WriteAsync("I'll skip the result execution");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Will not reach here
        }
    }

    public class AddVersionResultFilter : Attribute, IResultFilter
    {
        private readonly string version;

        public AddVersionResultFilter(string version)
        {
            this.version = version;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(
                "MVC-Version", new StringValues(this.version));
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }

    public class GreetingServiceFilter : IActionFilter
    {
        private readonly IGreetingService _greetingService;

        public GreetingServiceFilter(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["param"] = _greetingService.Greet("James Bond");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class GreetingTypeFilter : IActionFilter
    {
        private readonly IGreetingService _greetingService;

        public GreetingTypeFilter(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["param"] = _greetingService.Greet("Dr. No");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class GreetingTypeFilterWrapper : TypeFilterAttribute
    {
        public GreetingTypeFilterWrapper() : base(typeof(GreetingTypeFilter))
        {
        }
    }
}
