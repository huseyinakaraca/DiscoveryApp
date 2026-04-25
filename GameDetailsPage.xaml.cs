using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class GameDetailsPage : ContentPage
{
    private readonly ApiService _apiService;
    private Game _currentGame;
    public GameDetailsPage(Game selectedGame)
    {
        InitializeComponent();
        _apiService = new ApiService();
        _currentGame = selectedGame;
        this.BindingContext = _currentGame;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var fullDetails = await _apiService.GetGameDetailsAsync(_currentGame.Id);
        if (fullDetails != null)
        {
            _currentGame.Description = fullDetails.Description;
            this.BindingContext = null;
            this.BindingContext = _currentGame;
        }
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}