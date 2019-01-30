using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace _17_MVC_MODEL_BINDING_CUSTOM
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDataProtection();
            serviceCollection.AddMvc(mvcOptions =>
            {
                mvcOptions.ModelBinderProviders.Insert(0, new ProtectedIdModelBinderProvider());
                mvcOptions.Filters.Add(typeof(ProtectedIdResultFilter));
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

    public interface IProtectedIdAttribute
    {

    }

    public class ProtectedIdAttribute : Attribute, IProtectedIdAttribute
    {

    }

    public class ProtectedIdModelBinder : IModelBinder
    {
        private readonly IDataProtector _dataProtector;

        public ProtectedIdModelBinder(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("protect_my_query_string");
        }

        public Task BindModelAsync(ModelBindingContext modelBindingContext)
        {
            var valueProviderResult = modelBindingContext.ValueProvider.GetValue(modelBindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;
            modelBindingContext.ModelState.SetModelValue(modelBindingContext.ModelName, valueProviderResult);
            modelBindingContext.Result = ModelBindingResult.Success(_dataProtector.Unprotect(valueProviderResult.FirstValue));
            return Task.CompletedTask;
        }
    }

    public class ProtectedIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext modelBinderProviderContext)
        {
            if (modelBinderProviderContext.Metadata.IsComplexType)
                return null;
            var propertyName = modelBinderProviderContext.Metadata.PropertyName;
            if (propertyName == null)
                return null;
            var propertyInfo = modelBinderProviderContext.Metadata.ContainerType.GetProperty(propertyName);
            if (propertyInfo == null)
                return null;
            var attribute = propertyInfo.GetCustomAttributes(typeof(IProtectedIdAttribute), false).FirstOrDefault();
            if (attribute == null)
                return null;
            return new BinderTypeModelBinder(typeof(ProtectedIdModelBinder));
        }
    }

    public class ProtectedIdResultFilter : IResultFilter
    {
        private readonly IDataProtector _dataProtector;

        public ProtectedIdResultFilter(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("protect_my_query_string");
        }

        public void OnResultExecuting(ResultExecutingContext resultExecutingContext)
        {
            var viewResult = resultExecutingContext.Result as ViewResult;
            if (viewResult == null)
                return;
            if (!typeof(IEnumerable).IsAssignableFrom(viewResult.Model.GetType()))
                return;
            var model = viewResult.Model as IList;
            foreach (var item in model)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    var attribute = property.GetCustomAttributes(typeof(IProtectedIdAttribute), false).FirstOrDefault();
                    if (attribute != null)
                    {
                        property.SetValue(item, _dataProtector.Protect(property.GetValue(item).ToString()));
                    }
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext resultExecutedContext)
        {

        }
    }

    public class ProtectedIdResultFilterAttribute : TypeFilterAttribute
    {
        public ProtectedIdResultFilterAttribute() : base(typeof(ProtectedIdResultFilter))
        {

        }
    }

    public class MovieInputModel
    {
        [ProtectedId]
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class MovieViewModel
    {
        [ProtectedId]
        public string Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
    }
}
