using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace GptBarv2.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            var categories = new List<CategoryModel>
            {
                new CategoryModel { Name = "Gin" },
                new CategoryModel { Name = "Vodka" },
                new CategoryModel { Name = "Whisky" },
                new CategoryModel { Name = "Tekila" },
                new CategoryModel { Name = "Rom" },
            };

            CategoryCollection.ItemsSource = categories;
        }

        private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedCategory = e.CurrentSelection[0] as CategoryModel;
                if (selectedCategory != null)
                {
                    // CategoryDetailPage'e navigasyon
                    string route = $"CategoryDetailPage?category={selectedCategory.Name}";
                    await Shell.Current.GoToAsync(route);
                }
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }

    public class CategoryModel
    {
        public string Name { get; set; }
    }
}
