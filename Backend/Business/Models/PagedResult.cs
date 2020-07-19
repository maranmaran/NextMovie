using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Business.Models
{
    public class PagedResult<T> where T : class
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }

    }
}