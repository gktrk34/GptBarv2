using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(BrandName), "brandName")]
    public partial class BrandDetailPage : ContentPage
    {
        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set
            {
                _brandName = value;
                OnPropertyChanged();
                LoadBrandData();
            }
        }

        public BrandDetailPage()
        {
            InitializeComponent();
        }

        private void LoadBrandData()
        {
            BrandNameLabel.Text = $"Marka: {_brandName}";

            // Mock ürün listesi
            var allProducts = new List<ProductModel>
            {
                new ProductModel { Name="Gordon's London Dry", Brand="Gordon's" },
                new ProductModel { Name="Gordon's Premium Pink", Brand="Gordon's" },
                new ProductModel { Name="Beefeater London Dry", Brand="Beefeater" },
                new ProductModel { Name="Absolut Original", Brand="Absolut" },
                new ProductModel { Name="Absolut Citron", Brand="Absolut" },
                new ProductModel { Name="Grey Goose Original", Brand="Grey Goose" },
                new ProductModel { Name="Johnnie Walker Black Label", Brand="Johnnie Walker" },
                // ...
            };

            var filtered = allProducts
                .Where(p => p.Brand == _brandName)
                .ToList();

            ProductsCollection.ItemsSource = filtered;
        }

        private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedProduct = e.CurrentSelection[0] as ProductModel;
                if (selectedProduct != null)
                {
                    string route = $"ProductDetailPage?productName={selectedProduct.Name}";
                    await Shell.Current.GoToAsync(route);
                }
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }

    public class ProductModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
