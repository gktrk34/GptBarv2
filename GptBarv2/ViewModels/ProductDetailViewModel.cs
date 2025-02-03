using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GptBarv2.Models;
using GptBarv2.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace GptBarv2.ViewModels
{
    public partial class ProductDetailViewModel : ObservableObject
    {
        private readonly IProductRepository _productRepository;
        public ProductDetailViewModel()
        {
            _productRepository = MauiProgram.ServiceProvider.GetRequiredService<IProductRepository>();
            SimilarProducts = new ObservableCollection<ProductModel>();
            _productName = string.Empty;
            _productDescription = string.Empty;
            _productPrice = string.Empty;
            _tastingNotes = string.Empty;
            _additionalInfo = string.Empty;
            _star1 = "emptystar.png";
            _star2 = "emptystar.png";
            _star3 = "emptystar.png";
            _star4 = "emptystar.png";
            _star5 = "emptystar.png";
        }

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                SetProperty(ref _productName, value);
                _ = LoadProductAsync();
            }
        }

        private string _productDescription;
        public string ProductDescription
        {
            get => _productDescription;
            set => SetProperty(ref _productDescription, value);
        }

        private string _productPrice;
        public string ProductPrice
        {
            get => _productPrice;
            set => SetProperty(ref _productPrice, value);
        }

        private string _tastingNotes;
        public string TastingNotes
        {
            get => _tastingNotes;
            set => SetProperty(ref _tastingNotes, value);
        }

        private string _additionalInfo;
        public string AdditionalInfo
        {
            get => _additionalInfo;
            set => SetProperty(ref _additionalInfo, value);
        }

        public ObservableCollection<ProductModel> SimilarProducts { get; set; }

        private int _currentRating;
        public int CurrentRating
        {
            get => _currentRating;
            set
            {
                SetProperty(ref _currentRating, value);
                UpdateStarImages();
            }
        }

        private string _star1;
        public string Star1 { get => _star1; set => SetProperty(ref _star1, value); }
        private string _star2;
        public string Star2 { get => _star2; set => SetProperty(ref _star2, value); }
        private string _star3;
        public string Star3 { get => _star3; set => SetProperty(ref _star3, value); }
        private string _star4;
        public string Star4 { get => _star4; set => SetProperty(ref _star4, value); }
        private string _star5;
        public string Star5 { get => _star5; set => SetProperty(ref _star5, value); }

        private bool _isProductNotFound;
        public bool IsProductNotFound
        {
            get => _isProductNotFound;
            set => SetProperty(ref _isProductNotFound, value);
        }

        [RelayCommand]
        private async Task LoadProductAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(ProductName))
                {
                    var product = await _productRepository.GetByNameAsync(ProductName);
                    if (product != null)
                    {
                        ProductName = product.Name;
                        ProductDescription = product.Description;
                        ProductPrice = $"{product.Price:C}";
                        CurrentRating = product.Rating;
                        TastingNotes = product.TastingNotes;
                        AdditionalInfo = product.AdditionalInfo;
                        IsProductNotFound = false;

                        var similarList = await _productRepository.GetSimilarByCategoryAsync(product.Category, product.Name);
                        if (similarList != null && similarList.Count() > 0)  // CA1860: using Count() instead of Any()
                        {
                            SimilarProducts = new ObservableCollection<ProductModel>(similarList);
                            OnPropertyChanged(nameof(SimilarProducts));
                        }
                    }
                    else
                    {
                        IsProductNotFound = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading product: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Ürün bilgileri yüklenirken bir hata oluştu.", "OK");
            }
        }

        [RelayCommand]
        private async Task StarTappedAsync(int starCount)
        {
            try
            {
                CurrentRating = starCount;
                await _productRepository.UpdateRatingAsync(ProductName, starCount);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating rating: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Puan güncellenirken bir hata oluştu.", "OK");
            }
        }

        private void UpdateStarImages()
        {
            Star1 = CurrentRating >= 1 ? "fullstar.png" : "emptystar.png";
            Star2 = CurrentRating >= 2 ? "fullstar.png" : "emptystar.png";
            Star3 = CurrentRating >= 3 ? "fullstar.png" : "emptystar.png";
            Star4 = CurrentRating >= 4 ? "fullstar.png" : "emptystar.png";
            Star5 = CurrentRating >= 5 ? "fullstar.png" : "emptystar.png";
        }

        [RelayCommand]
        private async Task AddToFavoritesAsync()
        {
            try
            {
                await Shell.Current.DisplayAlert("Favoriler", $"{ProductName} favorilere eklendi!", "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding to favorites: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task AddToBarAsync()
        {
            try
            {
                await Shell.Current.DisplayAlert("Barım", $"{ProductName} barına eklendi!", "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding to bar: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SimilarProductTappedAsync(ProductModel product)
        {
            if (product == null) return;
            try
            {
                await Shell.Current.GoToAsync("..", false);
                await Shell.Current.GoToAsync($"ProductDetailPage?productName={product.Name}", false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to similar product: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Benzer ürün detayına geçişte bir hata oluştu.", "OK");
            }
        }
    }
}
