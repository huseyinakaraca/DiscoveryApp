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
        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = PasswordConfirmEntry.Text;
        // Bütün Alanlar Dolu Mu
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun!", "Tamam");
            return;
        }
        // Kullanýcý Adý 
        if (!username.All(c => char.IsLetterOrDigit(c) || c == '_'))
        {
            await DisplayAlert("Hata", "Kullanýcý adýnda boþluk veya özel sembol olamaz! Kelimeleri ayýrmak için sadece '_' (alt çizgi) kullanabilirsiniz (Örn: Ali_Enes).", "Tamam");
            return;
        }
        if (username.Length < 3 || username.Length > 20)
        {
            await DisplayAlert("Hata", "Kullanýcý adý en az 3, en fazla 20 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (!char.IsLetter(username[0]) || !char.IsLetter(username[1]) || !char.IsLetter(username[2]))
        {
            await DisplayAlert("Hata", "Kullanýcý adýnýn ilk 3 karakteri sadece harflerden oluþmalýdýr!", "Tamam");
            return;
        }
        if (!char.IsUpper(username[0]))
        {
            await DisplayAlert("Hata", "Kullanýcý adý büyük harfle baþlamalýdýr!", "Tamam");
            return;
        }
        // E-Posta 
        if (email.Length < 15 || email.Length > 40)
        {
            await DisplayAlert("Hata", "E-posta adresi en az 15, en fazla 40 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (email.Contains(" "))
        {
            await DisplayAlert("Hata", "E-posta adresinin içinde boþluk olamaz!", "Tamam");
            return;
        }
        if (!email.EndsWith("@gmail.com"))
        {
            await DisplayAlert("Hata", "Sadece @gmail.com uzantýlý e-posta adresleri ile kayýt olabilirsiniz!", "Tamam");
            return;
        }
        // Þifre Doðrulamasý
        if (password.Length < 8 || password.Length > 20)
        {
            await DisplayAlert("Hata", "Þifreniz en az 8, en fazla 20 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsUpper))
        {
            await DisplayAlert("Hata", "Þifrenizde en az 1 tane büyük harf bulunmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsLower))
        {
            await DisplayAlert("Hata", "Þifrenizde en az 1 tane küçük harf bulunmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsDigit))
        {
            await DisplayAlert("Hata", "Þifrenizde en az 1 tane rakam (sayý) bulunmalýdýr!", "Tamam");
            return;
        }
        // Þifreler Ayný Mý
        if (password != confirmPassword)
        {
            await DisplayAlert("Hata", "Girdiðiniz þifreler birbiriyle uyuþmuyor!", "Tamam");
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
            await DisplayAlert("Harika!", "Hesabýn baþarýyla oluþturuldu. Þimdi giriþ yapabilirsin.", "Tamam");
            App.Current.MainPage = new LoginPage();
        }
        else
        {
            await DisplayAlert("Hata", "Bu kullanýcý adý zaten alýnmýþ. Lütfen baþka bir kullanýcý adý deneyin.", "Tamam");
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
}