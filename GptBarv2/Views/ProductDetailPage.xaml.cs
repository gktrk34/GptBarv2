using GptBarv2.Models;
using GptBarv2.Repositories;
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

        private ProductModel _currentProduct;
        private int _currentRating = 0;

        // Benzer ürüne týklama event handler'ý
        private async void OnSimilarProductTapped(object sender, System.EventArgs e)
        {
            if (sender is BindableObject tappedObject && tappedObject.BindingContext is ProductModel tappedProduct)
            {
                // Önce mevcut sayfadan pop yapýp (geri gitmeden) sonra yeni ürünü gösterelim.
                await Shell.Current.GoToAsync("..", false);
                await Shell.Current.GoToAsync($"ProductDetailPage?productName={tappedProduct.Name}", false);
            }
        }

        public ProductDetailPage()
        {
            InitializeComponent();

            // DI (Dependency Injection) kullanarak IProductRepository örneðini alýyoruz
            _productRepo = MauiProgram.ServiceProvider.GetRequiredService<IProductRepository>();

            // BindingContext'i kendimize set ediyoruz
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(ProductName))
            {
                var product = await _productRepo.GetByNameAsync(ProductName);
                if (product != null)
                {
                    _currentProduct = product;

                    ProductNameLabel.Text = product.Name;
                    HeroImage.Source = product.ImageSource;
                    ProductDescriptionLabel.Text = product.Description;
                    ProductPriceLabel.Text = $"{product.Price:C}";

                    _currentRating = product.Rating;
                    System.Diagnostics.Debug.WriteLine($"[OnAppearing] Loaded product Rating = {_currentRating}");
                    UpdateRatingStars();

                    TastingNotesLabel.Text = product.TastingNotes;
                    AdditionalInfoLabel.Text = product.AdditionalInfo;
                    ProductNotFoundLabel.IsVisible = false;

                    // Benzer ürünleri veritabanýndan çekiyoruz
                    var similarList = await _productRepo.GetSimilarByCategoryAsync(product.Category, product.Name);
                    if (similarList != null && similarList.Any())
                    {
                        SimilarProducts = new ObservableCollection<ProductModel>(similarList);
                    }
                }
                else
                {
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

        // Yýldýz týklama event handler'ý
        private async void OnStarTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is string starCountString && int.TryParse(starCountString, out int starCount))
            {
                System.Diagnostics.Debug.WriteLine($"[OnStarTapped] starCount = {starCount}");
                await DisplayAlert("Rating", $"Tapped star: {starCount}", "OK");

                _currentRating = starCount;
                UpdateRatingStars();

                await _productRepo.UpdateRatingAsync(ProductName, starCount);
                System.Diagnostics.Debug.WriteLine($"[OnStarTapped] DB updated rating to {starCount}");

                if (_currentProduct != null)
                {
                    _currentProduct.Rating = starCount;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("[OnStarTapped] Parameter not valid");
            }
        }

        private void UpdateRatingStars()
        {
            var starImages = RatingContainer.Children.OfType<Image>();
            int index = 0;
            foreach (var star in starImages)
            {
                star.Source = index < _currentRating ? "fullstar.png" : "emptystar.png";
                index++;
            }
            System.Diagnostics.Debug.WriteLine($"[UpdateRatingStars] Current rating = {_currentRating}. Updated images.");
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
