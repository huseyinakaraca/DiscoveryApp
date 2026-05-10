namespace DiscoveryApp;
public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }
    private void OnResetPasswordClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        if (string.IsNullOrWhiteSpace(email))
        {
            DisplayAlert("Hata", "Lütfen e-posta adresinizi girin!", "Tamam");
            return;
        }
        if (email.Length < 15 || email.Length > 40)
        {
            DisplayAlert("Hata", "E-posta adresi en az 15, en fazla 40 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (email.Contains(" "))
        {
            DisplayAlert("Hata", "E-posta adresinin içinde boþluk olamaz!", "Tamam");
            return;
        }
        if (!email.EndsWith("@gmail.com"))
        {
            DisplayAlert("Hata", "Sadece @gmail.com uzantýlý e-posta adresleri geçerlidir!", "Tamam");
            return;
        }
        if (email != "huseyin@gmail.com")
        {
            DisplayAlert("Hata", "Sistemde böyle bir e-posta adresi bulunamadý!", "Tamam");
            return;
        }
        DisplayAlert("Bilgi", "Þifre sýfýrlama baðlantýsý e-posta adresinize gönderildi.", "Tamam");
        App.Current.MainPage = new LoginPage();
    }
    private void OnBackToLoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}