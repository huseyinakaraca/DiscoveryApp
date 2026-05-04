using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class PopularGamesPage : ContentPage
{
    private readonly ApiService _apiService;
    public PopularGamesPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        _apiService = new ApiService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.IsBusy = true;
        try
        {
            var games = await _apiService.GetTopRatedGamesAsync();
            PopularGamesList.ItemsSource = games;
        }
        finally
        {
            this.IsBusy = false;
        }
    }
    private async void OnGameSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Game selectedGame)
            return;
        await Navigation.PushAsync(new GameDetailsPage(selectedGame));

        ((CollectionView)sender).SelectedItem = null;
    }
}