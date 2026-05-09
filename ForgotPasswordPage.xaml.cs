namespace DiscoveryApp;
public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }
    private void OnResetPasswordClicked(object sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Eðer bu e-posta adresi sistemde varsa, þifre sýfýrlama baðlantýsý gönderildi.", "Tamam");
        App.Current.MainPage = new LoginPage();
    }
    private void OnBackToLoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}