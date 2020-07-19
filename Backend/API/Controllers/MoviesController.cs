using Business.Interfaces;
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
        [HttpGet]
        public async Task<IActionResult> GetTopRated(int page = 1, CancellationToken cancellationToken = default)
        {
            return Ok(await _tmdbMovieService.GetTopRated(page, cancellationToken));
        }

        /// <summary>
        /// Gets popular movies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPopular(int page = 1, CancellationToken cancellationToken = default)
        {
            return Ok(await _tmdbMovieService.GetPopular(page, cancellationToken));
        }

        /// <summary>
        /// Adds movie to the user list and whether the user liked it or not
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AddMovie(int movieId, bool liked, CancellationToken cancellationToken = default)
        {
            await _movieService.AddMovie(movieId, liked, cancellationToken);
            return Accepted();
        }

        /// <summary>
        /// Gets recommendations based on user movie preferences
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRecommendations(CancellationToken cancellationToken = default)
        {
            //var userId = User.Identity.Name;
            return Ok(await _movieService.GetRecommendations(cancellationToken));
        }
    }
}
