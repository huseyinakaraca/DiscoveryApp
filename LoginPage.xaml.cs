namespace DiscoveryApp;
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            DisplayAlert("Hata", "Lütfen kullanýcý adý ve þifre alanlarýný boþ býrakmayýn!", "Tamam");
            return;
        }
        if (username == "Admin" && password == "Admin1234a")
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            DisplayAlert("Hata", "Kullanýcý adý veya þifre yanlýþ!", "Tamam");
        }
    }
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ForgotPasswordPage();
    }
    private void OnSignUpClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new SignUpPage();
    }
}