using DiscoveryApp.Models;
using DiscoveryApp.Services;
using System.Text.Json;
namespace DiscoveryApp;
public partial class GameDetailsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly Game _currentGame;
    private string _originalDescription;
    public bool IsBusy { get; set; }
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
        IsBusy = true;
        this.BindingContext = null;
        this.BindingContext = this; 

        var fullDetails = await _apiService.GetGameDetailsAsync(_currentGame.Id);

        if (fullDetails != null)
        {
            string temizMetin = fullDetails.Description;
            if (!string.IsNullOrEmpty(temizMetin) && temizMetin.Contains("Español"))
            {
                temizMetin = temizMetin.Split("Español")[0].Trim();
            }
            _originalDescription = temizMetin;
            _currentGame.Description = _originalDescription;
            LanguagePicker.SelectedIndex = 0;
        }
        IsBusy = false;
        this.BindingContext = null;
        this.BindingContext = _currentGame;
    }
    private async void OnLanguageChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_originalDescription)) return;
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == 0) 
        {
            _currentGame.Description = _originalDescription;
            this.BindingContext = null;
            this.BindingContext = _currentGame;
            return;
        }
        IsBusy = true;
        this.BindingContext = null;
        this.BindingContext = this;
        string targetLang = "en";
        if (selectedIndex == 1) targetLang = "tr";
        else if (selectedIndex == 2) targetLang = "es";
        else if (selectedIndex == 3) targetLang = "de";
        else if (selectedIndex == 4) targetLang = "fr";
        try
        {
            string textToTranslate = _originalDescription;
            int maxKapasite = 400;
            System.Text.StringBuilder tamCeviri = new System.Text.StringBuilder();

            for (int i = 0; i < textToTranslate.Length; i += maxKapasite)
            {
                int parcaUzunlugu = Math.Min(maxKapasite, textToTranslate.Length - i);
                string parca = textToTranslate.Substring(i, parcaUzunlugu);

                string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(parca)}&langpair=en|{targetLang}&de=isubu@ogrenci.com";

                using var client = new HttpClient();
                var response = await client.GetStringAsync(url);

                using System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(response);
                if (doc.RootElement.GetProperty("responseStatus").GetInt32() == 200)
                {
                    var ceviriParcasi = doc.RootElement.GetProperty("responseData").GetProperty("translatedText").GetString();
                    tamCeviri.Append(ceviriParcasi + " ");
                }
                await Task.Delay(500);
            }
            _currentGame.Description = tamCeviri.ToString();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Sistem Hatasý", $"Çeviri baþarýsýz. Detay: {ex.Message}", "Tamam");
        }
        finally
        {
            IsBusy = false;
            this.BindingContext = null;
            this.BindingContext = _currentGame;
        }
    }
    private async void OnFavoriteButtonClicked(object sender, EventArgs e)
    {
        var dbService = new DatabaseService();
        await dbService.SaveFavoriteAsync(_currentGame);
        await DisplayAlert("Harika!", $"{_currentGame.Name} baþarýyla favorilerine eklendi.", "Tamam");
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void OnTrailerButtonClicked(object sender, EventArgs e)
    {
        string videoUrl = await _apiService.GetGameTrailerAsync(_currentGame.Id);
        if (!string.IsNullOrEmpty(videoUrl))
        {
            await Launcher.OpenAsync(videoUrl);
        }
        else
        {
            bool youtubeGit = await DisplayAlert(
                "Dýþ Baðlantýya Yönlendirme",
                "Bu oyunun fragmanýný izlemek için sizi YouTube'a yönlendireceðiz. Devam etmek istiyor musunuz?",
                "Evet, YouTube'a Git",
                "Vazgeç");
            if (youtubeGit)
            {
                string aramaKelimesi = Uri.EscapeDataString(_currentGame.Name + " trailer");
                string youtubeUrl = $"https://www.youtube.com/results?search_query={aramaKelimesi}";
                await Launcher.OpenAsync(youtubeUrl);
            }
        }
    }
}