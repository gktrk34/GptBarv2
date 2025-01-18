using GptBarv2.Models;
using GptBarv2.Repositories; // IProductRepository
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(ProductName), "productName")]
    public partial class ProductDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IProductRepository _productRepo;

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProductModel> _similarProducts = new();
        public ObservableCollection<ProductModel> SimilarProducts
        {
            get => _similarProducts;
            set
            {
                _similarProducts = value;
                OnPropertyChanged(nameof(SimilarProducts));
            }
        }

        private int _currentRating = 0;

        public ProductDetailPage(IProductRepository productRepo)
        {
            InitializeComponent();
            _productRepo = productRepo;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Veritabanýndan ürün çek
            if (!string.IsNullOrEmpty(ProductName))
            {
                var product = await _productRepo.GetByNameAsync(ProductName);
                if (product != null)
                {
                    // XAML'deki kontrolleri doldur
                    ProductNameLabel.Text = product.Name;
                    HeroImage.Source = product.ImageSource;
                    ProductDescriptionLabel.Text = product.Description;
                    ProductPriceLabel.Text = $"{product.Price:C}";

                    _currentRating = product.Rating;
                    UpdateRatingStars();

                    TastingNotesLabel.Text = product.TastingNotes;
                    AdditionalInfoLabel.Text = product.AdditionalInfo;
                    ProductNotFoundLabel.IsVisible = false;

                    // Benzer ürünleri de repo'dan çek
                    var similarList = await _productRepo.GetSimilarByCategoryAsync(product.Category, product.Name);
                    if (similarList != null && similarList.Any())
                    {
                        SimilarProducts = new ObservableCollection<ProductModel>(similarList);
                    }
                }
                else
                {
                    // Ürün yok
                    ProductNameLabel.Text = "Ürün Bulunamadý";
                    ProductDescriptionLabel.Text = "";
                    HeroImage.Source = "bar_hero.jpg";
                    ProductNotFoundLabel.IsVisible = true;
                }
            }
        }

        private void OnFavoriteClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Favoriler", $"{ProductName} favorilere eklendi!", "OK");
        }

        private void OnAddToBarClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Barým", $"{ProductName} barýna eklendi!", "OK");
        }

        private async void OnStarTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is int starCount)
            {
                _currentRating = starCount;
                UpdateRatingStars();

                // EF Core'da rating'i güncellemek istersen:
                await _productRepo.UpdateRatingAsync(ProductName, _currentRating);
            }
        }

        private void UpdateRatingStars()
        {
            var starImages = RatingContainer.Children.OfType<Image>();
            for (int i = 0; i < starImages.Count(); i++)
            {
                starImages.ElementAt(i).Source = i < _currentRating ? "fullstar.png" : "emptystar.png";
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
