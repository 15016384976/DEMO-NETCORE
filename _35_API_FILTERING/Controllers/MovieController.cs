using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace _35_API_FILTERING.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult Get(FilteringParams filteringParams)
        {
            var model = _service.GetMovies(filteringParams);
            var outputModel = new MovieOutputModel
            {
                Count = model.Count,
                Items = model.Select(m => ToMovieInfo(m)).ToList(),
            };
            return Ok(outputModel);
        }

        #region " Mappings "
        private MovieInfo ToMovieInfo(Movie model)
        {
            return new MovieInfo
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LeadActor = model.LeadActor,
                LastReadAt = DateTime.Now
            };
        }
        #endregion
    }
}
