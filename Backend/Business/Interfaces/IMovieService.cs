using Business.Models;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<UserMovie>> GetAllUserMovies(CancellationToken cancellationToken = default);
        Task AddMovie(int movieId, bool liked, CancellationToken cancellationToken = default);
        Task UpdateMovie(int movieId, bool liked, CancellationToken cancellationToken = default);
        Task DeleteMovie(int movieId, CancellationToken cancellationToken = default);
        Task<PagedResult<Movie>> GetRecommendations(int page = 1, CancellationToken cancellationToken = default);
    }
}
