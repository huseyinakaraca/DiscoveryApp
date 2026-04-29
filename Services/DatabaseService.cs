using SQLite;
using DiscoveryApp.Models;
namespace DiscoveryApp.Services;
public class DatabaseService
{
    private SQLiteAsyncConnection _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyGames.db3");
    public DatabaseService()
    {
        _db = new SQLiteAsyncConnection(_dbPath);

        _db.CreateTableAsync<Game>().Wait();
    }
    public Task<int> SaveFavoriteAsync(Game game)
    {
        return _db.InsertOrReplaceAsync(game);
    }
    public Task<int> DeleteFavoriteAsync(Game game)
    {
        return _db.DeleteAsync(game);
    }
    public Task<List<Game>> GetFavoritesAsync()
    {
        return _db.Table<Game>().ToListAsync();
    }
    public async Task<bool> IsFavoriteAsync(int gameId)
    {
        var game = await _db.Table<Game>().Where(x => x.Id == gameId).FirstOrDefaultAsync();
        return game != null;
    }
}