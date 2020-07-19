using Business.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITMDBMovieService
    {
        Task<PagedResult<Movie>> GetTopRated(int page = 1, CancellationToken cancellationToken = default);
        Task<PagedResult<Movie>> GetPopular(int page = 1, CancellationToken cancellationToken = default);
        Task<PagedResult<Movie>> GetRecommendations(int movieId, int page = 1, CancellationToken cancellationToken = default);
        Task<PagedResult<Movie>> GetSimilar(int movieId, int page = 1, CancellationToken cancellationToken = default);
    }
}
