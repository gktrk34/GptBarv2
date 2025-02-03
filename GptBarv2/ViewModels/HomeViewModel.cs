using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GptBarv2.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace GptBarv2.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<CategoryModel> Categories { get; } = new()
        {
            new CategoryModel { Name = "Gin", ImageSource = "gin.png" },
            new CategoryModel { Name = "Vodka", ImageSource = "vodka.png" },
            new CategoryModel { Name = "Whiskey", ImageSource = "whiskey.png" },
            new CategoryModel { Name = "Tekila", ImageSource = "tekila.png" },
            new CategoryModel { Name = "Rom", ImageSource = "rom.png" },
            new CategoryModel { Name = "Vermut", ImageSource = "vermut.png" },
            new CategoryModel { Name = "Likör", ImageSource = "likor.png" },
            new CategoryModel { Name = "Şarap", ImageSource = "sarap.png" },
            new CategoryModel { Name = "Brendi", ImageSource = "brendi.png" },
            new CategoryModel { Name = "Absent", ImageSource = "absent.png" },
            new CategoryModel { Name = "Diğer", ImageSource = "diger.png" }
        };

        [RelayCommand]
        private async Task CategoryTappedAsync(CategoryModel category)
        {
            if (category == null)
                return;
            try
            {
                string route = $"CategoryDetailPage?category={category.Name}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Kategoriye geçişte bir hata oluştu.", "OK");
            }
        }
    }
}
