namespace DiscoveryApp;
public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }
    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = PasswordConfirmEntry.Text;
        //Bütün Alanlar Dolu Mu
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun!", "Tamam");
            return;
        }
        // Kullanýcý Adý
        if (!username.All(c => char.IsLetterOrDigit(c) || c == '_'))
        {
            DisplayAlert("Hata", "Kullanýcý adýnda boþluk veya özel sembol olamaz! Kelimeleri ayýrmak için sadece '_' (alt çizgi) kullanabilirsiniz (Örn: Ali_Enes).", "Tamam");
            return;
        }
        if (username.Length < 3 || username.Length > 20)
        {
            DisplayAlert("Hata", "Kullanýcý adý en az 3, en fazla 20 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (!char.IsLetter(username[0]) || !char.IsLetter(username[1]) || !char.IsLetter(username[2]))
        {
            DisplayAlert("Hata", "Kullanýcý adýnýn ilk 3 karakteri sadece harflerden oluþmalýdýr!", "Tamam");
            return;
        }
        if (!char.IsUpper(username[0]))
        {
            DisplayAlert("Hata", "Kullanýcý adý büyük harfle baþlamalýdýr!", "Tamam");
            return;
        }
        // E-Posta 
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
            DisplayAlert("Hata", "Sadece @gmail.com uzantýlý e-posta adresleri ile kayýt olabilirsiniz!", "Tamam");
            return;
        }
        //Þifre
        if (password.Length < 8 || password.Length > 20)
        {
            DisplayAlert("Hata", "Þifreniz en az 8, en fazla 20 karakter olmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsUpper))
        {
            DisplayAlert("Hata", "Þifrenizde en az 1 tane büyük harf bulunmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsLower))
        {
            DisplayAlert("Hata", "Þifrenizde en az 1 tane küçük harf bulunmalýdýr!", "Tamam");
            return;
        }
        if (!password.Any(char.IsDigit))
        {
            DisplayAlert("Hata", "Þifrenizde en az 1 tane rakam (sayý) bulunmalýdýr!", "Tamam");
            return;
        }
        //Þifreler Ayný
        if (password != confirmPassword)
        {
            DisplayAlert("Hata", "Girdiðiniz þifreler birbiriyle uyuþmuyor!", "Tamam");
            return;
        }
        DisplayAlert("Harika!", "Kayýt iþlemi baþarýlý. (Veritabaný daha sonra baðlanacak)", "Tamam");
        App.Current.MainPage = new LoginPage();
    }
    private void OnBackToLoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}