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
    public async Task<List<Game>> SearchGamesAsync(string searchText)
    {
        string url = $"{BaseUrl}?search={searchText}&key={ApiKey}";

        try
        {
            string responseJson = await _httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<GameResponse>(responseJson);
            return data?.Games ?? new List<Game>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Arama yapılırken hata: {ex.Message}");
            return new List<Game>();
        }
    }
    public async Task<Game?> GetGameDetailsAsync(int gameId)
    {
        string url = $"{BaseUrl}/{gameId}?key={ApiKey}";
        try
        {
            string responseJson = await _httpClient.GetStringAsync(url);
            var gameData = JsonSerializer.Deserialize<Game>(responseJson);
            return gameData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Detay çekilirken hata oluştu: {ex.Message}");
            return null;
        }
    }
    public async Task<string?> GetGameTrailerAsync(int gameId)
    {
        string url = $"{BaseUrl}/{gameId}/movies?key={ApiKey}";
        try
        {
            string responseJson = await _httpClient.GetStringAsync(url);
            using System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(responseJson);
            var results = doc.RootElement.GetProperty("results");
            if (results.GetArrayLength() > 0)
            {
                return results[0].GetProperty("data").GetProperty("max").GetString();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fragman çekilirken hata oluştu: {ex.Message}");
            return null;
        }
    }
}