using Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMovieService
    {
        Task AddMovie(int movieId, bool liked, CancellationToken cancellationToken = default);
        Task<IEnumerable<Movie>> GetRecommendations(CancellationToken cancellationToken = default);
    }
}
