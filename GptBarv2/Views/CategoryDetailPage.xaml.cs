using GptBarv2.Models;
using GptBarv2.Repositories;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(CategoryName), "category")]
    public partial class CategoryDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IBrandRepository _brandRepository;

        private string _categoryName = string.Empty;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private ObservableCollection<BrandModel> _allBrands = new();

        private ObservableCollection<BrandModel> _brands = new();
        public ObservableCollection<BrandModel> Brands
        {
            get => _brands;
            set
            {
                _brands = value;
                OnPropertyChanged(nameof(Brands));
            }
        }

        public CategoryDetailPage(IBrandRepository brandRepository)
        {
            InitializeComponent();
            _brandRepository = brandRepository;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(CategoryName))
            {
                var brandList = await _brandRepository.GetAllByCategoryAsync(CategoryName);
                if (brandList != null && brandList.Any())
                {
                    _allBrands = new ObservableCollection<BrandModel>(brandList);
                    Brands = new ObservableCollection<BrandModel>(brandList.OrderBy(b => b.Name));
                }
                else
                {
                    await DisplayAlert("Uyarý", $"{CategoryName} kategorisine ait marka bulunamadý.", "OK");
                }
            }
        }

        private async void OnBrandTapped(object sender, TappedEventArgs e)
        {
            if (sender is Element element && element.BindingContext is BrandModel brand)
            {
                await Shell.Current.GoToAsync($"{nameof(BrandDetailPage)}?brandName={brand.Name}");
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Brands = new ObservableCollection<BrandModel>(_allBrands.OrderBy(b => b.Name));
            }
            else
            {
                var filtered = _allBrands
                    .Where(b => b.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(b => b.Name);

                Brands = new ObservableCollection<BrandModel>(filtered);
            }
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}