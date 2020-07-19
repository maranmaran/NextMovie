using Business.Interfaces;
using Business.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TMDBMovieService : ITMDBMovieService
    {
        private readonly TMDBSettings _settings;
        private readonly IHttpClientFactory _clientFactory;

        public TMDBMovieService(TMDBSettings settings, IHttpClientFactory clientFactory)
        {
            _settings = settings;
            _clientFactory = clientFactory;
        }

        public async Task<PagedResult<Movie>> GetTopRated(int page = 1, CancellationToken cancellationToken = default)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_settings.BaseUrl, "movie/top_rated", _settings.ApiKey) + $"&page={page}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PagedResult<Movie>>(responseStream, cancellationToken: cancellationToken);
        }

        public async Task<PagedResult<Movie>> GetPopular(int page = 1, CancellationToken cancellationToken = default)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_settings.BaseUrl, "movie/popular", _settings.ApiKey) + $"&page={page}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PagedResult<Movie>>(responseStream, cancellationToken: cancellationToken);
        }

        public async Task<PagedResult<Movie>> GetRecommendations(int movieId, int page = 1, CancellationToken cancellationToken = default)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_settings.BaseUrl, $"movie/{movieId}/recommendations", _settings.ApiKey) + $"&page={page}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PagedResult<Movie>>(responseStream, cancellationToken: cancellationToken);
        }

        public async Task<PagedResult<Movie>> GetSimilar(int movieId, int page = 1, CancellationToken cancellationToken = default)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_settings.BaseUrl, $"movie/{movieId}/similar", _settings.ApiKey) + $"&page={page}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PagedResult<Movie>>(responseStream, cancellationToken: cancellationToken);
        }
    }
}