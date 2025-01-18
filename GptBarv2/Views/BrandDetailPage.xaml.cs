using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GptBarv2.Models;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(BrandName), "brandName")]
    public partial class BrandDetailPage : ContentPage, INotifyPropertyChanged
    {
        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set
            {
                _brandName = value;
                OnPropertyChanged(nameof(BrandName));
                LoadBrandData();
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

        private ObservableCollection<ProductModel> _products;
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

        public BrandDetailPage()
        {
            InitializeComponent();

            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;
        }

        private void LoadBrandData()
        {
            // BrandNameLabel.Text = $"Marka: {_brandName}"; // Bu satýrý silin

            // Marka logosu atamasý
            BrandImage = _brandName.ToLower().Replace(" ", "") + ".png";

            var allProducts = new List<ProductModel>
    {
        new ProductModel { Name = "Gordon's London Dry", Brand = "Gordon's" },
        new ProductModel { Name = "Gordon's Premium Pink", Brand = "Gordon's" },
        new ProductModel { Name = "Beefeater London Dry", Brand = "Beefeater" },
        new ProductModel { Name = "Absolut Original", Brand = "Absolut" },
        new ProductModel { Name = "Absolut Citron", Brand = "Absolut" },
        new ProductModel { Name = "Grey Goose Original", Brand = "Grey Goose" },
    };

            var filteredProducts = allProducts
                .Where(p => p.Brand == _brandName)
                .ToList();

            Products = new ObservableCollection<ProductModel>(filteredProducts);
        }

        private async void OnProductTapped(ProductModel product)
        {
            if (product != null)
            {
                string route = $"ProductDetailPage?productName={product.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}