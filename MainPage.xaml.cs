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
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string keyword = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(keyword))
        {
            var popularGames = await _apiService.GetPopularGamesAsync();
            this.BindingContext = popularGames;
            return; 
        }
        var searchedGames = await _apiService.SearchGamesAsync(keyword);
        this.BindingContext = searchedGames;
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Çıkış", "Çıkmak istediğinize emin misiniz?", "Evet", "Hayır");
        if (confirm)
        {
            Application.Current.Quit();
        }
    }
    private async void OnGenreChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        int? genreId = null;
        switch (selectedIndex)
        {
            case 1: genreId = 4; break; 
            case 2: genreId = 3; break; 
            case 3: genreId = 5; break;  
            case 4: genreId = 10; break;       
            case 5: genreId = 15; break; 
            default: genreId = null; break;
        }
        var games = await _apiService.GetPopularGamesAsync(genreId);
        this.BindingContext = games;
    }
}