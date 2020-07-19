using Business.Interfaces;
using Business.Models;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (likedMovies == null || likedMovies.Count == 0)
            {
                throw new Exception("No movies");
            }

            var likedMoviesHash = new HashSet<int>(likedMovies.Select(x => x.Id));

            var results = new Dictionary<int, Movie>();

            foreach (var likedMovie in likedMovies)
            {
                var recommendationsList = await _tmdbMovieService.GetRecommendations(likedMovie.Id, cancellationToken: cancellationToken);

                foreach (var recommendation in recommendationsList.Results)
                {
                    if (!results.ContainsKey(recommendation.Id) && !likedMoviesHash.Contains(recommendation.Id))
                    {
                        results.Add(recommendation.Id, recommendation);
                    }
                }
            }

            // Sort by vote counts - amount of user votes
            // Sort by vote average - average grade of user votes
            // Sort by popularity - best user rated movies...most popular ones first
            var recommendedMovies = results.Values.ToList();
            recommendedMovies
                .OrderBy(x => x.VoteCount)
                .ThenBy(x => x.VoteAverage)
                .ThenBy(x => x.Popularity);

            return recommendedMovies.Take(20);
        }
    }
}