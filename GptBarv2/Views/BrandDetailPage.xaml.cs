using GptBarv2.Models;
using GptBarv2.Repositories;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(BrandName), "brandname")]
    public partial class BrandDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IBrandRepository _brandRepo;

        private string _brandName = string.Empty;
        public string BrandName
        {
            get => _brandName;
            set
            {
                _brandName = value;
                OnPropertyChanged(nameof(BrandName));
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

        public new event PropertyChangedEventHandler? PropertyChanged; //new anahtar kelimesi ile tanýmlayýn

        protected override void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                var brand = await _brandRepo.GetByNameAsync(BrandName);
                if (brand != null)
                {
                    BrandImage = brand.ImageSource;
                    Products = new ObservableCollection<ProductModel>(brand.Products);
                }
                else
                {
                    await DisplayAlert("Uyarý", $"{BrandName} markasý bulunamadý.", "OK");
                }
            }
        }

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                string route = $"ProductDetailPage?productname={product.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }

       
    }
}
