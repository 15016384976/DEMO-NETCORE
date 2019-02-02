using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace _29_MVC_TAG_HELPER_COMPONENT
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMetaService, MetaService>();

            serviceCollection.AddSingleton<ITagHelperComponent, MetaTagHelperComponent>();
            serviceCollection.AddSingleton<ITagHelperComponent, FooterTagHelperComponent>();
            serviceCollection.AddSingleton<ITagHelperComponent, ScriptsTagHelperComponent>();

            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }

    public interface IMetaService
    {
        Dictionary<string, string> GetMetadata();
    }

    public class MetaService : IMetaService
    {
        public Dictionary<string, string> GetMetadata()
        {
            return new Dictionary<string, string>
            {
                { "description", "This is a post on TagHelperComponent" },
                { "keywords", "asp.net core, tag helpers" }
            };
        }
    }

    public class MetaTagHelperComponent : TagHelperComponent
    {
        private readonly IMetaService _metaService;

        public MetaTagHelperComponent(IMetaService metaService)
        {
            _metaService = metaService;
        }

        public override int Order => 1;

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            if (string.Equals(tagHelperContext.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var metadata in _metaService.GetMetadata())
                {
                    tagHelperOutput.PostContent.AppendHtml($"<meta name=\"{metadata.Key}\" content=\"{metadata.Value}\" /> \r\n");
                }

            }
        }
    }

    public class FooterTagHelperComponent : TagHelperComponent
    {
        public override int Order => 1;

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            if (string.Equals(tagHelperContext.TagName, "footer", StringComparison.OrdinalIgnoreCase))
            {
                tagHelperOutput.PostContent.AppendHtml(string.Format($"<p><em>{DateTime.Now.ToString()}</em></p>"));
            }
        }
    }

    [HtmlTargetElement("footer")]
    public class FooterTagHelper : TagHelperComponentTagHelper
    {
        public FooterTagHelper(ITagHelperComponentManager tagHelperComponentManager, ILoggerFactory loggerFactory) : base(tagHelperComponentManager, loggerFactory) { }
    }

    public class ScriptsTagHelperComponent : TagHelperComponent
    {
        public override int Order => 99;

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            if (string.Equals(tagHelperContext.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                tagHelperOutput.PostContent.AppendHtml($"<script src='js/site.js'></script> \r\n");
            }
        }
    }
}
