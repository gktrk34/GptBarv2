using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GptBarv2.Models;
using GptBarv2.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace GptBarv2.ViewModels
{
    public partial class BrandDetailViewModel : ObservableObject
    {
        private readonly IBrandRepository _brandRepository;
        public BrandDetailViewModel()
        {
            _brandRepository = MauiProgram.ServiceProvider.GetRequiredService<IBrandRepository>();
            Products = new ObservableCollection<ProductModel>();
            _brandName = string.Empty;
            _brandImage = string.Empty;
        }

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set
            {
                SetProperty(ref _brandName, value);
                _ = LoadBrandAsync();
            }
        }

        private string _brandImage;
        public string BrandImage
        {
            get => _brandImage;
            set => SetProperty(ref _brandImage, value);
        }

        public ObservableCollection<ProductModel> Products { get; set; }

        [RelayCommand]
        private async Task ProductTappedAsync(ProductModel product)
        {
            if (product == null)
                return;
            try
            {
                await Shell.Current.GoToAsync($"ProductDetailPage?productName={product.Name}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to product detail: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Ürün detayına geçişte bir hata oluştu.", "OK");
            }
        }

        private async Task LoadBrandAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(BrandName))
                {
                    var brand = await _brandRepository.GetByNameAsync(BrandName);
                    if (brand != null)
                    {
                        BrandImage = brand.ImageSource;
                        Products = new ObservableCollection<ProductModel>(brand.Products);
                        OnPropertyChanged(nameof(Products));
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Uyarı", $"{BrandName} markası bulunamadı.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading brand: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Marka bilgileri yüklenirken bir hata oluştu.", "OK");
            }
        }
    }
}
