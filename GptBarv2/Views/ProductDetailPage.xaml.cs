using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GptBarv2.Models;
using GptBarv2.Repositories;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(ProductName), "productName")]
    public partial class ProductDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IProductRepository _productRepo;

        private string _productName = string.Empty;
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

        private string _brandImage = string.Empty;
        public string BrandImage
        {
            get => _brandImage;
            set
            {
                _brandImage = value;
                OnPropertyChanged(nameof(BrandImage));
            }
        }

        private ObservableCollection<ProductModel> _products = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ObservableCollection<ProductModel> SimilarProducts { get; set; } = new ObservableCollection<ProductModel>();

        public ICommand ProductTappedCommand { get; private set; }

        private int _currentRating = 0;

        public ProductDetailPage(IProductRepository productRepository)
        {
            InitializeComponent();

            _productRepo = productRepository;
            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;

        }

        private async void LoadProduct()
        {
            if (!string.IsNullOrEmpty(ProductName))
            {
                var product = await _productRepo.GetByNameAsync(ProductName);
                if (product != null)
                {
                    ProductNameLabel.Text = product.Name;
                    HeroImage.Source = product.ImageSource;
                    ProductDescriptionLabel.Text = product.Description;
                    ProductPriceLabel.Text = $"{product.Price:C}";
                    _currentRating = product.Rating;
                    UpdateRatingStars();
                    TastingNotesLabel.Text = product.TastingNotes;
                    AdditionalInfoLabel.Text = product.AdditionalInfo;
                    ProductNotFoundLabel.IsVisible = false;

                    var similarList = await _productRepo.GetSimilarByCategoryAsync(product.Category, product.Name);
                    if (similarList != null && similarList.Any())
                    {
                        SimilarProducts = new ObservableCollection<ProductModel>(similarList);
                    }
                    else
                    {
                        ProductNotFoundLabel.IsVisible = true;
                    }
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(ProductName))
            {
                var product = await _productRepo.GetByNameAsync(ProductName);
                if (product != null)
                {

                    ProductNameLabel.Text = product.Name;
                    HeroImage.Source = product.ImageSource;
                    ProductDescriptionLabel.Text = product.Description;
                    ProductPriceLabel.Text = $"{product.Price:C}";
                    _currentRating = product.Rating;
                    UpdateRatingStars();
                    TastingNotesLabel.Text = product.TastingNotes;
                    AdditionalInfoLabel.Text = product.AdditionalInfo;
                    ProductNotFoundLabel.IsVisible = false;

                    var similarList = await _productRepo.GetSimilarByCategoryAsync(product.Category, product.Name);
                    if (similarList != null && similarList.Any())
                    {
                        SimilarProducts = new ObservableCollection<ProductModel>(similarList);
                    }
                    else
                    {
                        ProductNotFoundLabel.IsVisible = true;
                    }
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

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?productName={product.Name}", false);
            }
        }

        private void OnStarTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is int starCount)
            {
                _currentRating = starCount;
                UpdateRatingStars();

                // Mevcut ürünü Products koleksiyonunda bul ve Rating özelliðini güncelle
                var product = Products.FirstOrDefault(p => p.Name == _productName);
                if (product != null)
                {
                    product.Rating = _currentRating;
                    OnPropertyChanged(nameof(Products)); // Bu, deðiþikliði arayüze bildirecek
                }
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
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected new virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}