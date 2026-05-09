namespace DiscoveryApp;
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Þifre sýfýrlama sayfasý daha sonra eklenecek.", "Tamam");
    }
    private void OnSignUpClicked(object sender, EventArgs e)
    {
        DisplayAlert("Bilgi", "Kayýt olma sayfasý daha sonra eklenecek.", "Tamam");
    }
}