using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace GptBarv2.Views
{
    public partial class HomePage : ContentPage
    {
        public ICommand CategoryTappedCommand { get; private set; }

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

            CategoryTappedCommand = new Command<CategoryModel>(OnCategoryTapped);

            BindingContext = this;
            CategoryCollection.ItemsSource = categories;
        }

        private async void OnCategoryTapped(CategoryModel category)
        {
            if (category != null)
            {
                string route = $"CategoryDetailPage?category={category.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }
    }

    public class CategoryModel
    {
        public string Name { get; set; }
    }
}
