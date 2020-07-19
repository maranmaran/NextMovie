using Business.Interfaces;
using Business.Models;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITMDBMovieService _tmdbMovieService;

        public MovieService(ApplicationDbContext context, ITMDBMovieService tmdbMovieService)
        {
            _context = context;
            _tmdbMovieService = tmdbMovieService;
        }

        public async Task AddMovie(int movieId, bool liked, CancellationToken cancellationToken = default)
        {
            var userMovie = new UserMovie()
            {
                Id = movieId,
                Liked = liked
            };

            await _context.AddAsync(userMovie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Movie>> GetRecommendations(CancellationToken cancellationToken = default)
        {
            var likedMovies = await _context.Movies.ToListAsync(cancellationToken);

            var results = new Dictionary<int, Movie>();

            foreach (var likedMovie in likedMovies)
            {
                var recommendationsList = await _tmdbMovieService.GetRecommendations(likedMovie.Id, cancellationToken: cancellationToken);

                foreach (var recommendation in recommendationsList.Results)
                {
                    if (!results.ContainsKey(recommendation.Id))
                    {
                        results.Add(recommendation.Id, recommendation);
                    }
                }
            }

            // do sorts...
            // do picks...
            // take top X...

            return results.Values;
        }
    }
}