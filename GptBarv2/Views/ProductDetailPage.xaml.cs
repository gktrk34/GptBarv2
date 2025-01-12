using Microsoft.Maui.Controls;
using System.Linq;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(ProductName), "productName")]
    public partial class ProductDetailPage : ContentPage
    {
        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged();
                LoadProduct();
            }
        }

        public ProductDetailPage()
        {
            InitializeComponent();
        }

        private void LoadProduct()
        {
            // Mock �r�n listesi
            var products = new[]
            {
                new { Name="Gordon's London Dry", Description="London dry gin..." },
                new { Name="Gordon's Premium Pink", Description="Fruity pink gin..." },
                new { Name="Absolut Original", Description="Swedish vodka..." },
                new { Name="Grey Goose Original", Description="French premium vodka..." },
                // ...
            };

            var product = products.FirstOrDefault(p => p.Name == _productName);
            if (product != null)
            {
                ProductNameLabel.Text = product.Name;
                ProductDescriptionLabel.Text = product.Description;
            }
            else
            {
                ProductNameLabel.Text = "�r�n Bulunamad�";
                ProductDescriptionLabel.Text = "";
            }
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            DisplayAlert("Favoriler", $"{_productName} favorilere eklendi!", "OK");
        }

        private void OnAddToBarClicked(object sender, EventArgs e)
        {
            DisplayAlert("Bar�m", $"{_productName} bar�na eklendi!", "OK");
        }
    }
}
