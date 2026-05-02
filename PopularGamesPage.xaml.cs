using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class PopularGamesPage : ContentPage
{
    private readonly ApiService _apiService;
    public PopularGamesPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var games = await _apiService.GetTopRatedGamesAsync();
        PopularGamesList.ItemsSource = games;
    }
    private async void OnGameSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Game selectedGame)
            return;
        await Navigation.PushAsync(new GameDetailsPage(selectedGame));

        ((CollectionView)sender).SelectedItem = null;
    }
}