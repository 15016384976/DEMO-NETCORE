using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace _27_MVC_TAG_HELPERS_CUSTOM
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGreetingService, GreetingService>();
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class EmployeesViewModel
    {
        public List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Profile { get; set; }

        public List<Friend> Friends { get; set; }
    }

    public class Friend
    {
        public string Name { get; set; }
    }

    [HtmlTargetElement("employee")]
    public class EmployeeTagHelper : TagHelper
    {
        [HtmlAttributeName("summary")]
        public string Summary { get; set; }

        [HtmlAttributeName("job-title")]
        public string JobTitle { get; set; }

        [HtmlAttributeName("profile")]
        public string Profile { get; set; }

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            tagHelperOutput.TagName = "details";
            tagHelperOutput.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<summary>{0}</summary>", Summary);
            sb.AppendFormat("<em>{0}</em>", JobTitle);
            sb.AppendFormat("<p>{0}</p>", Profile);
            sb.AppendFormat("<ul>");

            tagHelperOutput.PreContent.SetHtmlContent(sb.ToString());

            tagHelperOutput.PostContent.SetHtmlContent("</ul>");
        }
    }

    [HtmlTargetElement("friend", ParentTag = "employee", TagStructure = TagStructure.WithoutEndTag)]
    public class FriendTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            tagHelperOutput.TagName = "li";
            tagHelperOutput.TagMode = TagMode.StartTagAndEndTag;
            tagHelperOutput.Content.SetContent(Name);
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Summary { get; set; }
        public List<string> Stars { get; set; }
    }

    [HtmlTargetElement("movie", TagStructure = TagStructure.WithoutEndTag)]
    public class MovieTagHelper : TagHelper
    {
        [HtmlAttributeName("for-title")]
        public ModelExpression Title { get; set; }

        [HtmlAttributeName("for-year")]
        public ModelExpression ReleaseYear { get; set; }

        [HtmlAttributeName("for-director")]
        public ModelExpression Director { get; set; }

        [HtmlAttributeName("for-summary")]
        public ModelExpression Summary { get; set; }

        [HtmlAttributeName("for-stars")]
        public ModelExpression Stars { get; set; }

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            if (!(Stars.Model is List<string>))
                throw new ArgumentException("Stars must be a list");

            tagHelperOutput.TagName = "div";
            tagHelperOutput.TagMode = TagMode.StartTagAndEndTag;

            tagHelperOutput.Attributes.Add("class", "movie-tag");

            tagHelperOutput.Content.AppendHtml(GetTitle());
            tagHelperOutput.Content.AppendHtml(GetDirector());
            tagHelperOutput.Content.AppendHtml(GetSummary());
            tagHelperOutput.Content.AppendHtml(GetStars());
        }

        private TagBuilder GetTitle()
        {
            var year = new TagBuilder("span");
            year.Attributes.Add("class", "movie-year");
            year.InnerHtml.AppendHtml(string.Format("({0})", ReleaseYear.Model));

            var title = new TagBuilder("div");
            title.Attributes.Add("class", "movie-title");
            title.InnerHtml.AppendHtml(string.Format("{0}", Title.Model));
            title.InnerHtml.AppendHtml(year);

            return title;
        }

        private TagBuilder GetDirector()
        {
            var director = new TagBuilder("div");
            director.Attributes.Add("class", "movie-director");
            director.InnerHtml.AppendHtml(string.Format("<span>Director: {0}</span>", Director.Model));
            return director;
        }

        private TagBuilder GetSummary()
        {
            var summary = new TagBuilder("div");
            summary.Attributes.Add("class", "movie-summary");
            summary.InnerHtml.AppendHtml(string.Format("<span><strong>Plot: </strong>{0}</span>", Summary.Model));
            return summary;
        }

        private TagBuilder GetStars()
        {
            var stars = new TagBuilder("div");
            stars.Attributes.Add("class", "movie-stars");
            stars.InnerHtml.AppendHtml("<strong>Stars</strong>");
            stars.InnerHtml.AppendHtml("<ul>");

            var model = Stars.Model as List<string>;
            foreach (var item in model)
            {
                stars.InnerHtml.AppendHtml(string.Format("<li>{0}</li>", item));
            }

            stars.InnerHtml.AppendHtml("</ul>");
            return stars;
        }
    }

    [HtmlTargetElement("context-info")]
    public class ContextTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            tagHelperOutput.TagName = "div";
            tagHelperOutput.TagMode = TagMode.StartTagAndEndTag;

            tagHelperOutput.Content.AppendHtml(string.Format("<p>{0}</p>", ViewContext.ViewBag.Greeting));

            foreach (var item in ViewContext.RouteData.Values)
            {
                tagHelperOutput.Content.AppendHtml(string.Format("<p>{0}: {1}</p>", item.Key, item.Value));
            }

            tagHelperOutput.Content.AppendHtml(string.Format("<p>ModelState.IsValid: {0}</p>", ViewContext.ModelState.IsValid));
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

    [HtmlTargetElement("greeting")]
    public class GreetingTagHelper : TagHelper
    {
        private readonly IGreetingService _greetingService;

        public GreetingTagHelper(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override void Process(
            TagHelperContext context,
            TagHelperOutput output)
        {
            output.TagName = "p";
            output.Content.SetContent(_greetingService.Greet(Name));
        }
    }
}
