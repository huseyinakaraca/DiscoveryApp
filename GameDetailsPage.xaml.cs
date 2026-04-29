using DiscoveryApp.Models;
using DiscoveryApp.Services;
using System.Text.Json; // Gelen çeviri verisini (JSON) okumak için ekledik

namespace DiscoveryApp;

public partial class GameDetailsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly Game _currentGame;

    // YENÝ: Oyunun orijinal Ýngilizce açýklamasýný hafýzada tutmak için
    private string _originalDescription;

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
            // 1. ÇÖZÜM: RAWG'ýn yolladýðý gizli Ýspanyolca kýsmý kesip atýyoruz
            string temizMetin = fullDetails.Description;
            if (!string.IsNullOrEmpty(temizMetin) && temizMetin.Contains("Español"))
            {
                // "Español" kelimesinden öncesini al
                temizMetin = temizMetin.Split("Español")[0].Trim();
            }

            // Temizlenmiþ sadece Ýngilizce metni kasaya koy
            _originalDescription = temizMetin;
            _currentGame.Description = _originalDescription;

            // 2. ÇÖZÜM: Menü sayfa açýlýr açýlmaz boþ kalmasýn, 0. sýradakini (Ýngilizce) seçsin
            LanguagePicker.SelectedIndex = 0;

            this.BindingContext = null;
            this.BindingContext = _currentGame;
        }
    }

    // YENÝ: KULLANICI AÇILIR MENÜDEN (PICKER) DÝL DEÐÝÞTÝRDÝÐÝNDE ÇALIÞACAK KOD
    private async void OnLanguageChanged(object sender, EventArgs e)
    {
        // Eðer oyunun orijinal açýklamasý henüz internetten inmediyse iþlem yapma
        if (string.IsNullOrEmpty(_originalDescription)) return;

        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        string targetLang = "en"; // Varsayýlan: Ýngilizce

        // Hangi sýradaki dil seçildi? (0: Ýngilizce, 1: Türkçe, 2: Ýspanyolca ...)
        if (selectedIndex == 1) targetLang = "tr";
        else if (selectedIndex == 2) targetLang = "es";
        else if (selectedIndex == 3) targetLang = "de";
        else if (selectedIndex == 4) targetLang = "fr";

        if (targetLang != "en")
        {
            // Baþka bir dil seçildiyse çevirmen kuryesini yola çýkar
            try
            {
                // ÇÖZÜM 2.0: Metni parçalara bölüp çevirme (Chunking)
                string textToTranslate = _originalDescription;
                int maxKapasite = 400; // Kuryenin tek seferde taþýyabileceði güvenli sýnýr

                // Çevrilen parçalarý burada toplayýp birleþtireceðiz (Bantlama iþlemi)
                System.Text.StringBuilder tamCeviri = new System.Text.StringBuilder();

                // Metni 400 harflik parçalara bölerek kuryeyi defalarca yolluyoruz
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
        }
        else
        {
            _currentGame.Description = _originalDescription;
        }
        this.BindingContext = null;
        this.BindingContext = _currentGame;
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
}