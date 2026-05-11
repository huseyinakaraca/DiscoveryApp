using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class SignUpPage : ContentPage
{
    private readonly DatabaseService _dbService;
    public SignUpPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
    }
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        ErrorLabel.TextColor = Color.FromArgb("#E50914");
        ErrorLabel.IsVisible = false;
        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = PasswordConfirmEntry.Text;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            ShowErrorTemporarily("Lütfen tüm alanlarý doldurun!");
            return;
        }
        if (!username.All(c => char.IsLetterOrDigit(c) || c == '_'))
        {
            ShowErrorTemporarily("Kullanýcý adýnda boþluk veya özel sembol olamaz!");
            return;
        }
        if (username.Length < 3 || username.Length > 20)
        {
            ShowErrorTemporarily("Kullanýcý adý en az 3, en fazla 20 karakter olmalýdýr!");
            return;
        }
        if (!char.IsLetter(username[0]) || !char.IsLetter(username[1]) || !char.IsLetter(username[2]))
        {
            ShowErrorTemporarily("Kullanýcý adýnýn ilk 3 karakteri harf olmalýdýr!");
            return;
        }
        if (!char.IsUpper(username[0]))
        {
            ShowErrorTemporarily("Kullanýcý adý büyük harfle baþlamalýdýr!");
            return;
        }
        if (email.Length < 15 || email.Length > 40)
        {
            ShowErrorTemporarily("E-posta adresi 15-40 karakter arasýnda olmalýdýr!");
            return;
        }
        if (email.Contains(" "))
        {
            ShowErrorTemporarily("E-posta adresinin içinde boþluk olamaz!");
            return;
        }
        if (!email.EndsWith("@gmail.com"))
        {
            ShowErrorTemporarily("Sadece @gmail.com adresleri kabul edilir!");
            return;
        }
        if (password.Length < 8 || password.Length > 20)
        {
            ShowErrorTemporarily("Þifreniz 8-20 karakter arasýnda olmalýdýr!");
            return;
        }
        if (!password.Any(char.IsUpper))
        {
            ShowErrorTemporarily("Þifrenizde en az 1 tane büyük harf bulunmalýdýr!");
            return;
        }
        if (!password.Any(char.IsLower))
        {
            ShowErrorTemporarily("Þifrenizde en az 1 tane küçük harf bulunmalýdýr!");
            return;
        }
        if (!password.Any(char.IsDigit))
        {
            ShowErrorTemporarily("Þifrenizde en az 1 tane rakam bulunmalýdýr!");
            return;
        }
        if (password != confirmPassword)
        {
            ShowErrorTemporarily("Girdiðiniz þifreler birbiriyle uyuþmuyor!");
            return;
        }
        var newUser = new User
        {
            Username = username,
            Email = email,
            Password = password
        };
        bool isSuccess = await _dbService.RegisterUserAsync(newUser);
        if (isSuccess)
        {
            ErrorLabel.TextColor = Color.FromArgb("#2ECC71");
            ErrorLabel.Text = "Hesabýn baþarýyla oluþturuldu! Yönlendiriliyorsun...";
            ErrorLabel.IsVisible = true;
            await Task.Delay(1500);
            App.Current.MainPage = new LoginPage();
        }
        else
        {
            ShowErrorTemporarily("Bu kullanýcý adý zaten alýnmýþ!");
        }
    }
    private void OnBackToLoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
    private void OnTogglePasswordConfirmClicked(object sender, EventArgs e)
    {
        PasswordConfirmEntry.IsPassword = !PasswordConfirmEntry.IsPassword;
    }
    private void OnPasswordPressed(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = false;
    }
    private void OnPasswordReleased(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = true;
    }
    private void OnPasswordConfirmPressed(object sender, EventArgs e)
    {
        PasswordConfirmEntry.IsPassword = false;
    }
    private void OnPasswordConfirmReleased(object sender, EventArgs e)
    {
        PasswordConfirmEntry.IsPassword = true;
    }
    private async void ShowErrorTemporarily(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
        await Task.Delay(2000); 
        ErrorLabel.IsVisible = false;
    }
}