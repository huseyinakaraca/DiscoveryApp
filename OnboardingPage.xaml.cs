using System.Collections.ObjectModel;
namespace DiscoveryApp;
public class OnboardingItem
{
    public string Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
public partial class OnboardingPage : ContentPage
{
    public ObservableCollection<OnboardingItem> IntroItems { get; set; }
    public OnboardingPage()
    {
        InitializeComponent();
        IntroCarousel.IndicatorView = Noktalar;
        IntroItems = new ObservableCollection<OnboardingItem>
        {
            new OnboardingItem { Icon = "🎮", Title = "Oyunları Keşfet.", Description = "Binlerce oyun arasından sana en uygununu bul." },
            new OnboardingItem { Icon = "🗺️", Title = "Yeni Dünyaları Keşfet", Description = "Farklı evrenlerde geçen efsanevi hikayelere adım at." },
            new OnboardingItem { Icon = "📁", Title = "Kendi Kütüphaneni Oluştur.", Description = "Favori oyunlarını kaydet, puanla ve koleksiyonunu yönet." },
            new OnboardingItem { Icon = "👥", Title = "Birlikte Keşfedin", Description = "Kayıt ol ve DiscoveryApp topluluğuna katıl." }
        };
        BindingContext = this;
    }
    private void OnNextClicked(object sender, EventArgs e)
    {
        int guncelSira = IntroCarousel.Position;
        if (guncelSira < IntroItems.Count - 1)
        {
            IntroCarousel.ScrollTo(guncelSira + 1, position: ScrollToPosition.Center, animate: false);
        }
        else
        {
            Application.Current.MainPage = new AppShell();
        }
    }
    private void OnSkipClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
}