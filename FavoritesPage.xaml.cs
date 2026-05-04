using DiscoveryApp.Services;
using DiscoveryApp.Models;
namespace DiscoveryApp;
public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
        this.BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.IsBusy = true;
        try
        {
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
        finally
        {
            this.IsBusy = false;
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
    private async void OnFavoriteGameSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Game selectedGame)
            return;
        await Navigation.PushAsync(new GameDetailsPage(selectedGame));
        ((CollectionView)sender).SelectedItem = null;
    }
}