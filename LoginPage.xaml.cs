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
        App.Current.MainPage = new ForgotPasswordPage();
    }
    private void OnSignUpClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new SignUpPage();
    }
}