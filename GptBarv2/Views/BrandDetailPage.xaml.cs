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
        private readonly IBrandRepository _brandRepo;

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

        public BrandDetailPage(IBrandRepository brandRepo)
        {
            InitializeComponent();
            _brandRepo = brandRepo;

            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(BrandName))
            {
                // Markay� EF Core'dan �ek
                var brand = await _brandRepo.GetByNameAsync(BrandName);
                // GetByNameAsync tipik olarak .Include(b => b.Products) diyerek brand.Products'� da getirir

                if (brand != null)
                {
                    BrandImage = brand.ImageSource;
                    Products = new ObservableCollection<ProductModel>(brand.Products);
                }
                else
                {
                    await DisplayAlert("Uyar�", $"{BrandName} markas� bulunamad�.", "OK");
                }
            }
        }

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                // �r�n sayfas�na ge�
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
