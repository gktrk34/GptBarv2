using GptBarv2.Models;
using GptBarv2.Repositories; // IBrandRepository
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

        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        // T�m markalar (filtreleme i�in tutaca��z)
        private ObservableCollection<BrandModel> _allBrands = new();

        // Arama & g�r�nt�leme i�in
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

        public ICommand BrandTappedCommand { get; private set; }

        public CategoryDetailPage(IBrandRepository brandRepository)
        {
            InitializeComponent();
            _brandRepository = brandRepository;

            BrandTappedCommand = new Command<BrandModel>(OnBrandTapped);
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // CategoryName query property ile geldi
            if (!string.IsNullOrEmpty(CategoryName))
            {
                // EF Core'dan "CategoryName"e ait markalar� �ek
                var brandList = await _brandRepository.GetAllByCategoryAsync(CategoryName);
                if (brandList != null)
                {
                    _allBrands = new ObservableCollection<BrandModel>(brandList);
                    // Ba�lang��ta arama yok => hepsini g�sterece�iz
                    Brands = new ObservableCollection<BrandModel>(brandList.OrderBy(b => b.Name));
                }
            }
        }

        private async void OnBrandTapped(BrandModel brand)
        {
            if (brand != null)
            {
                // Markan�n detay sayfas�na git
                string route = $"BrandDetailPage?brandName={brand.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }

        // Arama �ubu�u
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Arama kutusu temizlendi -> t�m markalar� g�ster
                Brands = new ObservableCollection<BrandModel>(_allBrands.OrderBy(b => b.Name));
            }
            else
            {
                // Arama metni girilmi� -> filtreleyelim
                var filtered = _allBrands
                    .Where(b => b.Name.Contains(searchText, System.StringComparison.OrdinalIgnoreCase))
                    .OrderBy(b => b.Name);

                Brands = new ObservableCollection<BrandModel>(filtered);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
