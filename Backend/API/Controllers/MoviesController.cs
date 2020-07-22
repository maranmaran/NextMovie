using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// List of movies and recommendations based on user history
    /// </summary>
    public class MoviesController : BaseController
    {
        private readonly ITMDBMovieService _tmdbMovieService;
        private readonly IMovieService _movieService;

        /// <inheritdoc />
        public MoviesController(ITMDBMovieService tmdbMovieService, IMovieService movieService)
        {
            _tmdbMovieService = tmdbMovieService;
            _movieService = movieService;
        }

        /// <summary>
        /// Gets top rated movies
        /// </summary>
        [HttpGet("{page}")]
        public async Task<IActionResult> GetTopRated(int page = 1, CancellationToken cancellationToken = default)
        {
            return Ok(await _tmdbMovieService.GetTopRated(page, cancellationToken));
        }

        /// <summary>
        /// Gets popular movies
        /// </summary>
        [HttpGet("{page}")]
        public async Task<IActionResult> GetPopular(int page = 1, CancellationToken cancellationToken = default)
        {
            return Ok(await _tmdbMovieService.GetPopular(page, cancellationToken));
        }

        /// <summary>
        /// Gets recommendations based on user movie preferences
        /// </summary>
        [HttpGet("{page}")]
        public async Task<IActionResult> GetRecommendations(int page = 1, CancellationToken cancellationToken = default)
        {
            //var userId = User.Identity.Name;
            return Ok(await _movieService.GetRecommendations(page, cancellationToken));
        }

        /// <summary>
        /// Gets all user watched movies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUserMovies(CancellationToken cancellationToken = default)
        {
            //var userId = User.Identity.Name;
            return Ok(await _movieService.GetAllUserMovies(cancellationToken));
        }

        /// <summary>
        /// Adds movie to the user list and whether the user liked it or not
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] AddUpdateMovieRequest model, CancellationToken cancellationToken = default)
        {
            await _movieService.AddMovie(model.Id, model.Liked, cancellationToken);
            return Accepted();
        }

        /// <summary>
        /// Updates movie on the user list to new liking status
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] AddUpdateMovieRequest model, CancellationToken cancellationToken = default)
        {
            await _movieService.UpdateMovie(model.Id, model.Liked, cancellationToken);
            return Accepted();
        }

        /// <summary>
        /// Deletes movie from the user list of watched movies
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id, CancellationToken cancellationToken = default)
        {
            await _movieService.DeleteMovie(id, cancellationToken);
            return Accepted();
        }
    }
}
