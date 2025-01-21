namespace GptBarv2.Views;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    [Obsolete]
    private async void NavigateToMainPage()
    {
        if (Application.Current?.MainPage != null)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }

    private async void OnNavigateTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is string pageName)
        {
            switch (pageName)
            {
                case "HomePage":
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    break;
                case "CocktailsPage":
                    //await Shell.Current.GoToAsync($"//{nameof(CocktailsPage)}"); // E�er varsa
                    break;
                case "MyBarPage":
                    //await Shell.Current.GoToAsync($"//{nameof(MyBarPage)}"); // E�er varsa
                    break;
                case "FavoritesPage":
                    await Shell.Current.GoToAsync($"//{nameof(FavoritesPage)}");
                    break;
                case "ShoppingListPage":
                    //await Shell.Current.GoToAsync($"//{nameof(ShoppingListPage)}"); // E�er varsa
                    break;
                case "KnowledgeBasePage":
                    //await Shell.Current.GoToAsync($"//{nameof(KnowledgeBasePage)}"); // E�er varsa
                    break;
                case "SettingsPage":
                    //await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}"); // E�er varsa
                    break;
                default:
                    break;
            }
        }
    }
}