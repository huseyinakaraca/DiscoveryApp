using System.Text.Json.Serialization;
namespace DiscoveryApp.Models
{
    public class Game
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("released")]
        public string? ReleasedDate { get; set; }

        [JsonPropertyName("background_image")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }
    }
}
