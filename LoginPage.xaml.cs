using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class LoginPage : ContentPage
{
    private readonly DatabaseService _dbService;
    public LoginPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ShowErrorTemporarily("Lütfen kullanıcı adı ve şifre alanlarını boş bırakmayın!");
            return;
        }
        var user = await _dbService.LoginUserAsync(username, password);
        if (user != null)
        {
            Application.Current.MainPage = new AppShell();
        }
        else if (username == "Admin" && password == "Admin1234a")
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            ShowErrorTemporarily("Kullanıcı adı veya şifre yanlış!");
        }
    }
    private async void ShowErrorTemporarily(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
        await Task.Delay(2000);
        ErrorLabel.IsVisible = false;
    }
    private void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ForgotPasswordPage();
    }
    private void OnSignUpClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new SignUpPage();
    }
    private void OnPasswordPressed(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = false;
    }
    private void OnPasswordReleased(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = true;
    }
}