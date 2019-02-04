using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _33_API_CRUD.Controllers
{
    [Route("movies/{movieId}/reviews")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMovieService _movieService;

        public ReviewsController(
            IReviewService reviewService,
            IMovieService movieService)
        {
            _reviewService = reviewService;
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult Get(int movieId)
        {
            var model = _reviewService.GetReviews(movieId);
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(int movieId, int id)
        {
            var model = _reviewService.GetReview(movieId, id);
            if (model == null)
                return NotFound();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create(int movieId, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();
            if (!_movieService.MovieExists(movieId))
                return NotFound();
            var model = ToDomainModel(inputModel);
            _reviewService.AddReview(model);
            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetReview",
                new { movieId = outputModel.MovieId, id = outputModel.Id },
                outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int movieId, int id, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();
            if (!_reviewService.ReviewExists(movieId, id))
                return NotFound();
            var model = ToDomainModel(inputModel);
            _reviewService.UpdateReview(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int movieId, int id)
        {
            if (!_reviewService.ReviewExists(movieId, id))
                return NotFound();
            _reviewService.DeleteReview(id);
            return NoContent();
        }

        private ReviewOutputModel ToOutputModel(Review model)
        {
            return new ReviewOutputModel
            {
                Id = model.Id,
                Reviewer = model.Reviewer,
                Comments = model.Comments,
                MovieId = model.MovieId,
                LastReadAt = DateTime.Now
            };
        }

        private List<ReviewOutputModel> ToOutputModel(List<Review> model)
        {
            return model.Select(item => ToOutputModel(item)).ToList();
        }

        private Review ToDomainModel(ReviewInputModel inputModel)
        {
            return new Review
            {
                Id = inputModel.Id,
                Reviewer = inputModel.Reviewer,
                Comments = inputModel.Comments,
                MovieId = inputModel.MovieId
            };
        }
    }
}
