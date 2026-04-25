using System.Text.Json.Serialization;
namespace DiscoveryApp.Models
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("released")]
        public string? ReleasedDate { get; set; }
        [JsonPropertyName("background_image")]
        public string? ImageUrl { get; set; }
        [JsonPropertyName("rating")]
        public double Rating { get; set; }
        [JsonPropertyName("description_raw")]
        public string? Description { get; set; }
    }
}                