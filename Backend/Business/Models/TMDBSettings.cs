namespace Business.Models
{
    public class TMDBSettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; } = "https://api.themoviedb.org/3/{0}?api_key={1}";
        public string BaseImageUrl { get; set; } = "https://image.tmdb.org/t/p/w500/{1}";
    }
}