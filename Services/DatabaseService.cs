using SQLite;
using DiscoveryApp.Models;
namespace DiscoveryApp.Services;
public class DatabaseService
{
    private SQLiteAsyncConnection _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyGames.db3");
    public Task<List<User>> GetUsersAsync() => _db.Table<User>().ToListAsync();
    public Task<int> UpdateUserAsync(User user) => _db.UpdateAsync(user);
    public DatabaseService()
    {
        _db = new SQLiteAsyncConnection(_dbPath);
        _db.CreateTableAsync<Game>().Wait();
        _db.CreateTableAsync<User>().Wait();
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
    public async Task<bool> RegisterUserAsync(User user)
    {
        var existingUser = await _db.Table<User>().Where(u => u.Username == user.Username).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            return false;
        }
        await _db.InsertAsync(user);
        return true;
    }
    public async Task<User> LoginUserAsync(string username, string password)
    {
        var user = await _db.Table<User>()
                            .Where(u => u.Username == username && u.Password == password)
                            .FirstOrDefaultAsync();
        return user;
    }
}