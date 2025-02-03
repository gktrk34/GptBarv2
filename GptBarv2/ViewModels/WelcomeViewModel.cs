using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;

namespace GptBarv2.ViewModels
{
    public partial class WelcomeViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task NavigateAsync(string pageName)
        {
            try
            {
                switch (pageName)
                {
                    case "HomePage":
                        await Shell.Current.GoToAsync($"//HomePage");
                        break;
                    case "CocktailsPage":
                        await Shell.Current.GoToAsync($"//CocktailsPage");
                        break;
                    case "MyBarPage":
                        await Shell.Current.GoToAsync($"//MyBarPage");
                        break;
                    case "FavoritesPage":
                        await Shell.Current.GoToAsync($"//FavoritesPage");
                        break;
                    case "ShoppingListPage":
                        await Shell.Current.GoToAsync($"//ShoppingListPage");
                        break;
                    case "KnowledgeBasePage":
                        await Shell.Current.GoToAsync($"//KnowledgeBasePage");
                        break;
                    case "SettingsPage":
                        await Shell.Current.GoToAsync($"//SettingsPage");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Navigasyon sırasında bir hata oluştu.", "OK");
            }
        }
    }
}
