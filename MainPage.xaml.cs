using DiscoveryApp.Services;
using DiscoveryApp.Models;
using System.Collections.ObjectModel;
namespace DiscoveryApp;
public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;
    private string currentSort = "";
    private int? currentGenreId = null;
    private int _currentPage = 1;
    private bool _isLoadingMore = false;
    private ObservableCollection<Game> _gamesCollection = new ObservableCollection<Game>();
    public MainPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        _apiService = new ApiService();
        PopularGamesList.ItemsSource = _gamesCollection;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }
    private async Task LoadData(int? genreId = null, string search = null, bool isLoadMore = false)
    {
        if (this.IsBusy) return;
        this.IsBusy = true;
        try
        {
            if (!isLoadMore)
            {
                _currentPage = 1;
                _gamesCollection.Clear();
            }
            List<Game> games;
            if (!string.IsNullOrWhiteSpace(search))
            {
                games = await _apiService.SearchGamesAsync(search);
            }
            else
            {
                games = await _apiService.GetPopularGamesAsync(genreId, currentSort, _currentPage);
            }
            foreach (var game in games)
            {
                _gamesCollection.Add(game);
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Hata", "Oyunlar yüklenirken bir sorun oluştu.", "Tamam");
        }
        finally
        {
            this.IsBusy = false;
            _isLoadingMore = false;
        }
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
        await LoadData(search: e.NewTextValue, isLoadMore: false);
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Çıkış", "Çıkmak istediğinize emin misiniz?", "Evet", "Hayır");
        if (confirm)
        {
            Application.Current.Quit();
        }
    }
    private async void OnCategoryChanged(object sender, EventArgs e)
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
        currentGenreId = genreId;
        await LoadData(genreId: genreId);
    }
    private async void OnSortChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1)
        {
            currentSort = selectedIndex switch
            {
                0 => "name",       
                1 => "-released", 
                _ => ""           
            };
            await LoadData(genreId: currentGenreId);
        }
    }
    private async void OnScrolledToBottom(object sender, EventArgs e)
    {
        if (_isLoadingMore || this.IsBusy)
            return;
        _isLoadingMore = true;
        _currentPage++;
        await LoadData(genreId: currentGenreId, isLoadMore: true);
    }
}