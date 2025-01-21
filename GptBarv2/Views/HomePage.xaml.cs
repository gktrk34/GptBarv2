using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GptBarv2.Views
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<CategoryModel> Categories { get; private set; }
        public ICommand CategoryTappedCommand { get; private set; }

        public HomePage()
        {
            InitializeComponent();
            Categories = new ObservableCollection<CategoryModel>
            {
                new CategoryModel { Name = "Gin", ImageSource = "gin.png" },
                new CategoryModel { Name = "Vodka", ImageSource = "vodka.png" },
                new CategoryModel { Name = "Whiskey", ImageSource = "whiskey.png" },
                new CategoryModel { Name = "Tekila", ImageSource = "tekila.png" },
                new CategoryModel { Name = "Rom", ImageSource = "rom.png" },
                new CategoryModel { Name = "Vermut", ImageSource = "vermut.png" },
                new CategoryModel { Name = "Likör", ImageSource = "likor.png" },
                new CategoryModel { Name = "Þarap", ImageSource = "sarap.png" },
                new CategoryModel { Name = "Brendi", ImageSource = "brendi.png" },
                new CategoryModel { Name = "Absent", ImageSource = "absent.png" },
                new CategoryModel { Name = "Diðer", ImageSource = "diger.png" }
            };
            CategoryTappedCommand = new Command<CategoryModel>(OnCategoryTapped);
            BindingContext = this;
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
        public string Name { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
    }
}
