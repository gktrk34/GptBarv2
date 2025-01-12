using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

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

        public ICommand ProductTappedCommand { get; private set; }

        public BrandDetailPage()
        {
            InitializeComponent();

            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;
        }

        private void LoadBrandData()
        {
            BrandNameLabel.Text = $"Marka: {_brandName}";

            var allProducts = new List<ProductModel>
            {
                new ProductModel { Name = "Gordon's London Dry", Brand = "Gordon's" },
                new ProductModel { Name = "Gordon's Premium Pink", Brand = "Gordon's" },
                new ProductModel { Name = "Beefeater London Dry", Brand = "Beefeater" },
                new ProductModel { Name = "Absolut Original", Brand = "Absolut" },
                new ProductModel { Name = "Absolut Citron", Brand = "Absolut" },
                new ProductModel { Name = "Grey Goose Original", Brand = "Grey Goose" },
            };

            var filtered = allProducts
                .Where(p => p.Brand == _brandName)
                .ToList();

            ProductsCollection.ItemsSource = filtered;
        }

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                string route = $"ProductDetailPage?productName={product.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }
    }

    public class ProductModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
