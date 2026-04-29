using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var dbService = new DatabaseService();
        var favoriteGames = await dbService.GetFavoritesAsync();
        if (favoriteGames == null || favoriteGames.Count == 0)
        {
            EmptyLabel.IsVisible = true;
            FavoritesCollectionView.IsVisible = false;
        }
        else
        {
            EmptyLabel.IsVisible = false;
            FavoritesCollectionView.IsVisible = true;
            FavoritesCollectionView.ItemsSource = favoriteGames;
        }
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var gameToDelete = (DiscoveryApp.Models.Game)button.CommandParameter;
        var dbService = new DatabaseService();
        await dbService.DeleteFavoriteAsync(gameToDelete);
        OnAppearing();
    }
}