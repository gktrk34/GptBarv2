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
    public partial class CategoryDetailViewModel : ObservableObject
    {
        private readonly IBrandRepository _brandRepository;
        public CategoryDetailViewModel()
        {
            _brandRepository = MauiProgram.ServiceProvider.GetRequiredService<IBrandRepository>();
            Brands = new ObservableCollection<BrandModel>();
            _categoryName = string.Empty;
            _searchText = string.Empty;
        }

        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                SetProperty(ref _categoryName, value);
                _ = LoadBrandsAsync();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                    FilterBrands();
            }
        }

        private ObservableCollection<BrandModel> _allBrands = new();
        public ObservableCollection<BrandModel> Brands { get; set; }

        [RelayCommand]
        private async Task BrandTappedAsync(BrandModel brand)
        {
            if (brand == null) return;
            try
            {
                await Shell.Current.GoToAsync($"BrandDetailPage?brandName={brand.Name}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to brand detail: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Marka detayına geçişte bir hata oluştu.", "OK");
            }
        }

        private async Task LoadBrandsAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(CategoryName))
                {
                    var brandList = await _brandRepository.GetAllByCategoryAsync(CategoryName);
                    if (brandList != null && brandList.Count() > 0)  // CA1860: using Count() instead of Any()
                    {
                        _allBrands = new ObservableCollection<BrandModel>(brandList);
                        Brands = new ObservableCollection<BrandModel>(_allBrands.OrderBy(b => b.Name));
                        OnPropertyChanged(nameof(Brands));
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Uyarı", $"{CategoryName} kategorisine ait marka bulunamadı.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading brands: {ex.Message}");
                await Shell.Current.DisplayAlert("Hata", "Markalar yüklenirken bir hata oluştu.", "OK");
            }
        }

        private void FilterBrands()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                Brands = new ObservableCollection<BrandModel>(_allBrands.OrderBy(b => b.Name));
            else
            {
                var filtered = _allBrands.Where(b => b.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                                          .OrderBy(b => b.Name);
                Brands = new ObservableCollection<BrandModel>(filtered);
            }
            OnPropertyChanged(nameof(Brands));
        }
    }
}
