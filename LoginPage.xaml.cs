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
            DisplayAlert("Hata", "Lütfen kullanıcı adı ve şifre alanlarını boş bırakmayın!", "Tamam");
            return;
        }
        if (username == "Admin" && password == "Admin1234a")
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            DisplayAlert("Hata", "Kullanıcı adı veya şifre yanlış!", "Tamam");
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
    private void OnPasswordPressed(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = false; 
    }
    private void OnPasswordReleased(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = true;  
    }
}