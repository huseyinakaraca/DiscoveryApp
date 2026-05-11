using DiscoveryApp.Models;
using DiscoveryApp.Services;
namespace DiscoveryApp;
public partial class SettingsPage : ContentPage
{
    private readonly DatabaseService _dbService;
    private User _currentUser;
    public SettingsPage()
    {
        InitializeComponent();
        _dbService = new DatabaseService();
        LoadUserData();
    }
    private async void LoadUserData()
    {
        var users = await _dbService.GetUsersAsync();
        _currentUser = users.FirstOrDefault();
        if (_currentUser != null)
        {
            UsernameLabel.Text = _currentUser.Username;
            EmailLabel.Text = _currentUser.Email;
        }
    }
    private async void OnUpdatePasswordClicked(object sender, EventArgs e)
    {
        string newPass = NewPasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(newPass) || newPass.Length < 8 || newPass.Length > 20)
        {
            ShowStatusTemporarily("Þifre en az 8, en fazla 20 karakter olmalýdýr!", "#E50914");
            return;
        }
        if (!newPass.Any(char.IsUpper))
        {
            ShowStatusTemporarily("Þifrenizde en az 1 tane büyük harf bulunmalýdýr!", "#E50914");
            return;
        }
        if (!newPass.Any(char.IsLower))
        {
            ShowStatusTemporarily("Þifrenizde en az 1 tane küçük harf bulunmalýdýr!", "#E50914");
            return;
        }
        if (!newPass.Any(char.IsDigit))
        {
            ShowStatusTemporarily("Þifrenizde en az 1 tane rakam (sayý) bulunmalýdýr!", "#E50914");
            return;
        }
        _currentUser.Password = newPass;
        await _dbService.UpdateUserAsync(_currentUser);
        ShowStatusTemporarily("Þifre baþarýyla güncellendi!", "#2ECC71");
        NewPasswordEntry.Text = ""; 
    }
    private async void ShowStatusTemporarily(string message, string colorHex)
    {
        StatusLabel.Text = message;
        StatusLabel.TextColor = Color.FromArgb(colorHex);
        StatusLabel.IsVisible = true;
        await Task.Delay(3000);
        StatusLabel.IsVisible = false;
    }
}