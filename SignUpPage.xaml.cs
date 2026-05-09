namespace DiscoveryApp;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }
    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Harika!", "Kayýt iþlemi baþarýlý. (Veritabaný daha sonra baðlanacak)", "Tamam");

        App.Current.MainPage = new LoginPage();
    }
    private void OnBackToLoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}