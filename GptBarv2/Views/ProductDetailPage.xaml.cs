using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GptBarv2.Models;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(ProductName), "productName")]
    public partial class ProductDetailPage : ContentPage, INotifyPropertyChanged
    {
        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged();
                LoadProduct();
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

        public ObservableCollection<ProductModel> SimilarProducts { get; set; } = new ObservableCollection<ProductModel>();

        public ICommand ProductTappedCommand { get; private set; }

        private int _currentRating = 0;

        public ProductDetailPage()
        {
            InitializeComponent();

            ProductTappedCommand = new Command<ProductModel>(OnProductTapped);
            BindingContext = this;

        }


        private void LoadProduct()
        {
            // �rnek �r�n verileri
            var products = new List<ProductModel>
            {
                 new ProductModel { Name = "Gordon's London Dry", Description = "Klasik London Dry gin; yo�un bitkisel aromalar.", ImageSource = "gordonslondondry.png", Price = 120.00, Rating = 4, Category = "Gin", TastingNotes = "Yo�un ard��, ki�ni� ve melekotu aromalar�. Damakta narenciye ve baharatl� notalar.", AdditionalInfo = "Alkol Oran�: %43\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Gordon's Premium Pink", Description = "Meyvemsi notalar ve hafif �i�eksi aromalar.", ImageSource = "gordonspremiumpink.png", Price = 135.00, Rating = 4, Category = "Gin", TastingNotes = "�ilek, ahududu ve frenk �z�m� aromalar�. Hafif tatl� ve ferahlat�c�.", AdditionalInfo = "Alkol Oran�: %37.5\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Beefeater London Dry", Description = "Geleneksel bir London Dry Gin, belirgin ard�� ve narenciye notalar�.", ImageSource = "beefeaterlondondry.png", Price = 140.00, Rating = 4, Category = "Gin", TastingNotes = "Ard��, limon kabu�u ve ki�ni� aromalar�. Damakta kuru ve baharatl� bir biti�.", AdditionalInfo = "Alkol Oran�: %40\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Tanqueray London Dry", Description = "Zengin ve baharatl�, klasik bir cin.", ImageSource = "tanqueraylondondry.png", Price = 155.00, Rating = 4, Category = "Gin", TastingNotes = "Ard��, melekotu k�k� ve meyan k�k� aromalar�. Damakta yo�un ve kal�c�.", AdditionalInfo = "Alkol Oran�: %47.3\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Hendrick's Gin", Description = "Salatal�k ve g�l yapraklar�yla zenginle�tirilmi� �zg�n bir cin.", ImageSource = "hendricksgin.png", Price = 180.00, Rating = 4, Category = "Gin", TastingNotes = "Salatal�k, g�l, ard�� ve narenciye aromalar�. Damakta yumu�ak ve ferahlat�c�.", AdditionalInfo = "Alkol Oran�: %44\n�retim Yeri: �sko�ya" },
                new ProductModel { Name = "Bombay Sapphire", Description = "Egzotik baharatlar ve bitkilerle zenginle�tirilmi�, dengeli bir cin.", ImageSource = "bombaysapphire.png", Price = 165.00, Rating = 4, Category = "Gin", TastingNotes = "Badem, limon kabu�u, meyan k�k� ve zencefil aromalar�. Damakta yumu�ak ve dengeli.", AdditionalInfo = "Alkol Oran�: %40\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "The Botanist Islay Dry Gin", Description = "Islay Adas�'ndan gelen 22 farkl� bitki ile �retilen kompleks bir cin.", ImageSource = "thebotanist.png", Price = 220.00, Rating = 4, Category = "Gin", TastingNotes = "Yo�un bitkisel ve �i�eksi aromalar. Ard��, nane, ki�ni� ve limon notalar�. Damakta zengin ve katmanl�.", AdditionalInfo = "Alkol Oran�: %46\n�retim Yeri: �sko�ya" },
                new ProductModel { Name = "Roku Gin", Description = "Japonya'dan gelen 6 �zel bitki ile �retilen, zarif ve dengeli bir cin.", ImageSource = "rokugin.png", Price = 190.00, Rating = 4, Category = "Gin", TastingNotes = "Sakura �i�e�i, yuzu kabu�u, sencha �ay�, gyokuro �ay�, sansho biberi ve ard�� aromalar�. Damakta taze ve �i�eksi.", AdditionalInfo = "Alkol Oran�: %43\n�retim Yeri: Japonya" },
                new ProductModel { Name = "Monkey 47 Schwarzwald Dry Gin", Description = "47 farkl� bitki ile �retilen, Almanya'n�n Kara Orman b�lgesinden gelen kompleks bir cin.", ImageSource = "monkey47.png", Price = 350.00, Rating = 4, Category = "Gin", TastingNotes = "Yo�un baharat, �i�ek ve meyve aromalar�. Ard��, zencefil, lavanta ve greyfurt notalar�. Damakta zengin ve uzun.", AdditionalInfo = "Alkol Oran�: %47\n�retim Yeri: Almanya" },
                new ProductModel { Name = "Nolet's Silver Dry Gin", Description = "Hollanda'dan gelen, g�l, �eftali ve ahududu gibi meyvemsi ve �i�eksi notalara sahip modern bir cin.", ImageSource = "nolets.png", Price = 300.00, Rating = 4, Category = "Gin", TastingNotes = "T�rk g�l�, �eftali, ahududu ve ard�� aromalar�. Damakta yumu�ak, meyvemsi ve �i�eksi.", AdditionalInfo = "Alkol Oran�: %47.6\n�retim Yeri: Hollanda" },
                new ProductModel { Name = "Plymouth Gin", Description = "�ngiltere'nin Plymouth �ehrinden gelen, yumu�ak ve dengeli bir cin.", ImageSource = "plymouth.png", Price = 150.00, Rating = 4, Category = "Gin", TastingNotes = "Ard��, ki�ni�, portakal ve kakule aromalar�. Damakta yumu�ak ve hafif tatl�ms�.", AdditionalInfo = "Alkol Oran�: %41.2\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Sipsmith London Dry Gin", Description = "K���k partiler halinde bak�r imbiklerde �retilen, geleneksel bir London Dry Gin.", ImageSource = "sipsmith.png", Price = 175.00, Rating = 4, Category = "Gin", TastingNotes = "Ard��, limon kabu�u, badem ve tar��n aromalar�. Damakta kuru, baharatl� ve dengeli.", AdditionalInfo = "Alkol Oran�: %41.6\n�retim Yeri: �ngiltere" },
                new ProductModel { Name = "Aviation American Gin", Description = "Amerika'dan gelen, ard���n daha az bask�n oldu�u, yumu�ak ve baharatl� bir cin.", ImageSource = "aviation.png", Price = 160.00, Rating = 4, Category = "Gin", TastingNotes = "Lavanta, portakal kabu�u, kakule ve ki�ni� aromalar�. Damakta yumu�ak ve baharatl�.", AdditionalInfo = "Alkol Oran�: %42\n�retim Yeri: ABD" },
                new ProductModel { Name = "Citadelle Gin", Description = "Fransa'dan gelen, 19 farkl� baharat ve bitki ile zenginle�tirilmi�, zarif bir cin.", ImageSource = "citadelle.png", Price = 145.00, Rating = 4, Category = "Gin", TastingNotes = "Narenciye, baharat, �i�ek ve ard�� aromalar�. Damakta dengeli ve ferahlat�c�.", AdditionalInfo = "Alkol Oran�: %44\n�retim Yeri: Fransa" },
                new ProductModel { Name = "Absolut Original", Description = "Saf �sve� votkas�, p�r�zs�z bir doku.", ImageSource = "absolutoriginal.png", Price = 90.00, Rating = 4, Category = "Vodka", TastingNotes = "Temiz ve n�tr aroma profili, hafif tah�l notalar�.", AdditionalInfo = "Alkol Oran�: %40\n�retim Yeri: �sve�" }
            };

            var product = products.FirstOrDefault(p => p.Name == _productName);
            if (product != null)
            {
                ProductNameLabel.Text = product.Name;
                HeroImage.Source = product.ImageSource;
                ProductDescriptionLabel.Text = product.Description;
                ProductPriceLabel.Text = $"{product.Price:C}";
                _currentRating = (int)product.Rating;
                UpdateRatingStars();
                TastingNotesLabel.Text = product.TastingNotes;
                AdditionalInfoLabel.Text = product.AdditionalInfo;
                ProductNotFoundLabel.IsVisible = false;

                SimilarProducts = new ObservableCollection<ProductModel>(products.Where(p => p.Category == product.Category && p.Name != product.Name)
                    .Select(p => new ProductModel { Name = p.Name, ImageSource = p.ImageSource }));

                OnPropertyChanged(nameof(SimilarProducts));
            }
            else
            {
                ProductNameLabel.Text = "�r�n Bulunamad�";
                ProductDescriptionLabel.Text = "";
                HeroImage.Source = "bar_hero.jpg";
                ProductNotFoundLabel.IsVisible = true;
            }
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            DisplayAlert("Favoriler", $"{_productName} favorilere eklendi!", "OK");
        }

        private void OnAddToBarClicked(object sender, EventArgs e)
        {
            DisplayAlert("Bar�m", $"{_productName} bar�na eklendi!", "OK");
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

        private void OnStarTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is int starCount)
            {
                _currentRating = starCount;
                UpdateRatingStars();

                // G�ncel �r�n� bul ve Rating �zelli�ini g�ncelle
                var product = Products.FirstOrDefault(p => p.Name == _productName);
                if (product != null)
                {
                    product.Rating = _currentRating;
                    OnPropertyChanged(nameof(Products)); // Products koleksiyonunun de�i�ti�ini bildir.
                }
            }
        }

        private void UpdateRatingStars()
        {
            var starImages = RatingContainer.Children.OfType<Image>();
            for (int i = 0; i < starImages.Count(); i++)
            {
                starImages.ElementAt(i).Source = i < _currentRating ? "fullstar.png" : "emptystar.png";
            }
        }
    }
}