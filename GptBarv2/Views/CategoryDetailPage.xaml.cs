using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GptBarv2.Models;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(CategoryName), "category")]
    public partial class CategoryDetailPage : ContentPage, INotifyPropertyChanged
    {
        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
                LoadBrands(value);
            }
        }

        private ObservableCollection<BrandModel> _brands;
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

        public CategoryDetailPage()
        {
            InitializeComponent();
            BrandTappedCommand = new Command<BrandModel>(OnBrandTapped);
            BindingContext = this;
        }

        private void LoadBrands(string categoryName)
        {
            List<BrandModel> allBrands = new List<BrandModel>();
            if (categoryName == "Gin")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Gordon's", Category = "Gin" },
                    new BrandModel { Name = "Tanqueray", Category = "Gin" },
                    new BrandModel { Name = "Beefeater", Category = "Gin" },
                    new BrandModel { Name = "Hendrick's", Category = "Gin" },
                    new BrandModel { Name = "Bombay Sapphire", Category = "Gin" },
                    new BrandModel { Name = "The Botanist", Category = "Gin" },
                    new BrandModel { Name = "Roku Gin", Category = "Gin" },
                    new BrandModel { Name = "Monkey 47", Category = "Gin" },
                    new BrandModel { Name = "Nolet's", Category = "Gin" },
                    new BrandModel { Name = "Plymouth", Category = "Gin" },
                    new BrandModel { Name = "Sipsmith", Category = "Gin" },
                    new BrandModel { Name = "Aviation", Category = "Gin" },
                    new BrandModel { Name = "Citadelle", Category = "Gin" }
                    // ... Di�er cin markalar�n� ekleyin
                };
            }
            else if (categoryName == "Vodka")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Absolut", Category = "Vodka" },
                    new BrandModel { Name = "Smirnoff", Category = "Vodka" },
                    new BrandModel { Name = "Grey Goose", Category = "Vodka" },
                    // ... Di�er vodka markalar�n� ekleyin
                };
            }
            else if (categoryName == "Whiskey")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Jack Daniel's", Category = "Whiskey" },
                    new BrandModel { Name = "Johnnie Walker", Category = "Whiskey" },
                    new BrandModel { Name = "Jameson", Category = "Whiskey" },
                    // ... Di�er whiskey markalar�n� ekleyin
                };
            }
            else if (categoryName == "Rom")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Bacardi", Category = "Rom" },
                    new BrandModel { Name = "Havana Club", Category = "Rom" },
                    new BrandModel { Name = "Captain Morgan", Category = "Rom" },
                    // ... Di�er rom markalar�n� ekleyin
                };
            }
            else if (categoryName == "Tekila")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Don Julio", Category = "Tekila" },
                    new BrandModel { Name = "Patron", Category = "Tekila" },
                    new BrandModel { Name = "Jose Cuervo", Category = "Tekila" },
                    // ... Di�er tekila markalar�n� ekleyin
                };
            }
            else if (categoryName == "Vermut")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Martini", Category = "Vermut" },
                    new BrandModel { Name = "Cinzano", Category = "Vermut" },
                    // ... Di�er vermut markalar�n� ekleyin
                };
            }
            else if (categoryName == "Lik�r")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Baileys", Category = "Lik�r" },
                    new BrandModel { Name = "J�germeister", Category = "Lik�r" },
                    new BrandModel { Name = "Kahlua", Category = "Lik�r" },
                    new BrandModel { Name = "Cointreau", Category = "Lik�r" },
                    // ... Di�er lik�r markalar�n� ekleyin
                };
            }
            else if (categoryName == "�arap")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Kavakl�dere", Category = "�arap" },
                    new BrandModel { Name = "Doluca", Category = "�arap" },
                    // ... Di�er �arap markalar�n� ekleyin
                };
            }
            else if (categoryName == "Brendi")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Hennessy", Category = "Brendi" },
                    new BrandModel { Name = "R�my Martin", Category = "Brendi" },
                    new BrandModel { Name = "Courvoisier", Category = "Brendi" },
                    // ... Di�er brendi markalar�n� ekleyin
                };
            }
            else if (categoryName == "Absent")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Pernod Absinthe", Category = "Absent" },
                    new BrandModel { Name = "K�bler", Category = "Absent" },
                    // ... Di�er absent markalar�n� ekleyin
                };
            }
            else if (categoryName == "Di�er")
            {
                allBrands = new List<BrandModel>
                {
                    new BrandModel { Name = "Aperol", Category = "Di�er" },
                    new BrandModel { Name = "Campari", Category = "Di�er" },
                    // ... Di�er markalar� ekleyin
                };
            }

            var orderedBrands = allBrands.OrderBy(b => b.Name).ToList();
            if (Brands == null)
            {
                Brands = new ObservableCollection<BrandModel>(orderedBrands);
            }
            else
            {
                Brands.Clear();
                foreach (var brand in orderedBrands)
                {
                    Brands.Add(brand);
                }
            }
            OnPropertyChanged(nameof(Brands));
        }

        private async void OnBrandTapped(BrandModel brand)
        {
            if (brand != null)
            {
                string route = $"BrandDetailPage?brandName={brand.Name}";
                await Shell.Current.GoToAsync(route);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadBrands(CategoryName);
            }
            else
            {
                var filteredBrands = Brands
                    .Where(b => b.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Brands = new ObservableCollection<BrandModel>(filteredBrands);
                OnPropertyChanged(nameof(Brands));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}