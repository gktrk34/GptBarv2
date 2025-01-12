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
            // �rnek mock �r�n dizisi; her �r�n i�in temsili data kullan�l�yor.
            var products = new[]
            {
                new { Name = "Gordon's London Dry", Description = "Klasik London Dry gin; yo�un bitkisel aromalar.", Image = "dotnet_bot.png" },
                new { Name = "Gordon's Premium Pink", Description = "Meyvemsi notalar ve hafif �i�eksi aromalar.", Image = "dotnet_bot.png" },
                new { Name = "Absolut Original", Description = "Saf �sve� votkas�, p�r�zs�z bir doku.", Image = "dotnet_bot.png" },
            };

            var product = products.FirstOrDefault(p => p.Name == _productName);
            if (product != null)
            {
                ProductNameLabel.Text = product.Name;
                ProductImage.Source = product.Image;
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
