using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _32_MVC_RAZOR_PAGES.Views.Movie
{
    public class EditModel : PageModel
    {
        private readonly IMovieService service;

        public EditModel(IMovieService service)
        {
            this.service = service;
        }

        [BindProperty]
        public MovieInputModel Movie { get; set; }

        public IActionResult OnGet(int id)
        {
            var model = this.service.GetMovie(id);
            if (model == null)
                return RedirectToPage("./Index");

            this.Movie = new MovieInputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var model = new _32_MVC_RAZOR_PAGES.Movie
            {
                Id = this.Movie.Id,
                Title = this.Movie.Title,
                ReleaseYear = this.Movie.ReleaseYear,
                Summary = this.Movie.Summary
            };
            service.UpdateMovie(model);

            return RedirectToPage("./Index");
        }
    }
}