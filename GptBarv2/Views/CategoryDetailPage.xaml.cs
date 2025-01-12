using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(CategoryName), "category")]
    public partial class CategoryDetailPage : ContentPage
    {
        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged();
                LoadCategoryData();
            }
        }

        public CategoryDetailPage()
        {
            InitializeComponent();
        }

        private void LoadCategoryData()
        {
            CategoryNameLabel.Text = $"Kategori: {_categoryName}";

            // Örnek marka verileri
            var allBrands = new List<BrandModel>
            {
                new BrandModel { Name="Gordon's", Category="Gin" },
                new BrandModel { Name="Beefeater", Category="Gin" },
                new BrandModel { Name="Absolut", Category="Vodka" },
                new BrandModel { Name="Grey Goose", Category="Vodka" },
                new BrandModel { Name="Jack Daniel's", Category="Whisky" },
                new BrandModel { Name="Johnnie Walker", Category="Whisky" },
                new BrandModel { Name="Don Julio", Category="Tekila" },
                new BrandModel { Name="Patrón", Category="Tekila" },
            };

            var filtered = allBrands
                .Where(b => b.Category == _categoryName)
                .ToList();

            BrandsCollection.ItemsSource = filtered;
        }

        private async void OnBrandSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedBrand = e.CurrentSelection[0] as BrandModel;
                if (selectedBrand != null)
                {
                    string route = $"BrandDetailPage?brandName={selectedBrand.Name}";
                    await Shell.Current.GoToAsync(route);
                }
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }

    public class BrandModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
