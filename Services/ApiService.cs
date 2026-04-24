using System.Text.Json;
using DiscoveryApp.Models; 
namespace DiscoveryApp.Services;
public class ApiService
{
    private const string ApiKey = "cfdc7f31a02d4b9393c9e289ba726f2d";
    private const string BaseUrl = "https://api.rawg.io/api/games";
    private readonly HttpClient _httpClient;
    public ApiService()
    {
        _httpClient = new HttpClient();
    }
    public async Task<List<Game>> GetPopularGamesAsync()
    {
        string url = $"{BaseUrl}?key={ApiKey}";
        try
        {
            string responseJson = await _httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<GameResponse>(responseJson);
            return data?.Games ?? new List<Game>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu: {ex.Message}");
            return new List<Game>();
        }
    }
}