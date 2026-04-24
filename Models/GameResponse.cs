using System.Text.Json.Serialization;
namespace DiscoveryApp.Models;
public class GameResponse
{
    [JsonPropertyName("results")]
    public List<Game>? Games { get; set; }
}