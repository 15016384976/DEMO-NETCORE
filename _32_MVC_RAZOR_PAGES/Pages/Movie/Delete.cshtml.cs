using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _32_MVC_RAZOR_PAGES.Views.Movie
{
    public class DeleteModel : PageModel
    {
        private readonly IMovieService service;

        public DeleteModel(IMovieService service)
        {
            this.service = service;
        }

        [BindProperty]
        public int Id { get; set; }
        public string Title { get; set; }

        public IActionResult OnGet(int id)
        {
            var model = this.service.GetMovie(id);
            if (model == null)
                return RedirectToPage("./Index");

            this.Id = model.Id;
            this.Title = model.Title;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!service.MovieExists(this.Id))
                return RedirectToPage("./Index");

            service.DeleteMovie(this.Id);

            return RedirectToPage("./Index");
        }
    }
}