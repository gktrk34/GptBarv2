using GptBarv2.Models;
using GptBarv2.Repositories;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(BrandName), "brandName")]
    public partial class BrandDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IBrandRepository _brandRepository;

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set
            {
                _brandName = value;
                OnPropertyChanged(nameof(BrandName));
            }
        }

        private string _brandImage;
        public string BrandImage
        {
            get => _brandImage;
            set
            {
                _brandImage = value;
                OnPropertyChanged(nameof(BrandImage));
            }
        }

        private ObservableCollection<ProductModel> _products = new();
        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ICommand ProductTappedCommand { get; private set; }

        public BrandDetailPage(IBrandRepository brandRepository)
        {
            InitializeComponent();
            _brandRepository = brandRepository;
            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // BrandName QueryProperty ile geliyor
            if (!string.IsNullOrEmpty(BrandName))
            {
                // Markayý DB'den çek
                var brand = await _brandRepository.GetByNameAsync(BrandName);
                if (brand != null)
                {
                    // Marka logoyu xaml'de göstermek
                    BrandImage = brand.ImageSource;
                    // Markanýn ürünlerini listede göstermek
                    Products = new ObservableCollection<ProductModel>(brand.Products);
                }
                else
                {
                    // brand = null => markaya dair veri yok
                    await DisplayAlert("Uyarý", $"{BrandName} markasý bulunamadý.", "OK");
                }
            }
        }

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                // Ürün detay sayfasýna git
                string route = $"ProductDetailPage?productName={product.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
