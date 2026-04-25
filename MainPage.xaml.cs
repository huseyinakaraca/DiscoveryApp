using DiscoveryApp.Services;
using DiscoveryApp.Models;
namespace DiscoveryApp;
public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;
    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var games = await _apiService.GetPopularGamesAsync();
        this.BindingContext = games;
    }
    private async void OnGameSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Game selectedGame)
            return;
        await Navigation.PushAsync(new GameDetailsPage(selectedGame));
        ((CollectionView)sender).SelectedItem = null;
    }
}