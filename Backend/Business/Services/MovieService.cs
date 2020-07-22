using Business.Interfaces;
using Business.Models;
using Domain;
using Domain.Entities;
using Infrastructure.Exceptions;
using LazyCache;
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
        private readonly int _pageSize = 50;
        private readonly IAppCache _cache;

        public MovieService(ApplicationDbContext context, ITMDBMovieService tmdbMovieService, IAppCache cache)
        {
            _context = context;
            _tmdbMovieService = tmdbMovieService;
            _cache = cache;
        }

        public async Task<IEnumerable<UserMovie>> GetAllUserMovies(CancellationToken cancellationToken = default)
        {
            return await _context.Movies.ToListAsync(cancellationToken);
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

        public async Task UpdateMovie(int movieId, bool liked, CancellationToken cancellationToken = default)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId, cancellationToken);
            if (movie == null)
                throw new NotFoundException<int>(movieId);

            movie.Liked = liked;
            _context.Update(movie);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMovie(int movieId, CancellationToken cancellationToken = default)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId, cancellationToken);
            if (movie == null)
                throw new NotFoundException<int>(movieId);

            _context.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedResult<Movie>> GetRecommendations(int page = 1, CancellationToken cancellationToken = default)
        {
            var likedMovies = await _context.Movies.ToListAsync(cancellationToken);
            if (likedMovies == null || likedMovies.Count == 0)
            {
                throw new Exception("No movies");
            }

            var likedMoviesHash = new HashSet<int>(likedMovies.Select(x => x.Id));

            var results = new Dictionary<int, Movie>();

            var recommendationsPage = (int)Math.Ceiling((double)page / likedMovies.Count);

            var cachedEntry = _cache.Get<PagedResult<Movie>>(ResolveRecommendationsCacheKey(recommendationsPage, likedMovies.Count));
            if (cachedEntry != null)
            {
                var cachedResult = new PagedResult<Movie>()
                {
                    Page = page,
                    TotalPages = cachedEntry.TotalPages,
                    Results = cachedEntry.Results.Skip((page - 1) * _pageSize).Take(_pageSize).ToList()
                };

                return cachedResult;
            }

            var totalPages = 0;
            while (results.Values.Count < page * _pageSize)
            {
                foreach (var likedMovie in likedMovies)
                {
                    var recommendationsList = await _tmdbMovieService.GetRecommendations(likedMovie.Id, recommendationsPage, cancellationToken: cancellationToken);

                    foreach (var recommendation in recommendationsList.Results)
                    {
                        if (!results.ContainsKey(recommendation.Id) && !likedMoviesHash.Contains(recommendation.Id))
                        {
                            results.Add(recommendation.Id, recommendation);
                        }
                    }

                    totalPages += recommendationsList.TotalPages;
                }
            }

            // Sort by vote counts - amount of user votes
            // Sort by vote average - average grade of user votes
            // Sort by popularity - best user rated movies...most popular ones first
            var recommendedMovies = results.Values.ToList();
            recommendedMovies = recommendedMovies
                .OrderByDescending(x => x.VoteCount)
                .ThenBy(x => x.VoteAverage)
                .ThenBy(x => x.Popularity)
                .ToList();

            var entryToCache = new PagedResult<Movie>()
            {
                TotalPages = totalPages,
                Results = recommendedMovies
            };

            _cache.Add(ResolveRecommendationsCacheKey(recommendationsPage, likedMovies.Count), entryToCache);

            return new PagedResult<Movie>()
            {
                Page = 1,
                TotalPages = totalPages,
                Results = recommendedMovies.Skip(page - 1 * 20).Take(_pageSize).ToList(),
            };
            ;
        }

        private string ResolveRecommendationsCacheKey(int recommendationsPage, int likedMoviesCount)
        {
            //return $"{recommendationsPage}|{likedMoviesCount}";
            return $"{recommendationsPage}";
        }
    }
}