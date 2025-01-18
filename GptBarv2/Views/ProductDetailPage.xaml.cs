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
            // Örnek ürün verileri
            var products = new List<ProductModel>
            {
                 new ProductModel { Name = "Gordon's London Dry", Description = "Klasik London Dry gin; yoðun bitkisel aromalar.", ImageSource = "gordonslondondry.png", Price = 120.00, Rating = 4, Category = "Gin", TastingNotes = "Yoðun ardýç, kiþniþ ve melekotu aromalarý. Damakta narenciye ve baharatlý notalar.", AdditionalInfo = "Alkol Oraný: %43\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Gordon's Premium Pink", Description = "Meyvemsi notalar ve hafif çiçeksi aromalar.", ImageSource = "gordonspremiumpink.png", Price = 135.00, Rating = 4, Category = "Gin", TastingNotes = "Çilek, ahududu ve frenk üzümü aromalarý. Hafif tatlý ve ferahlatýcý.", AdditionalInfo = "Alkol Oraný: %37.5\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Beefeater London Dry", Description = "Geleneksel bir London Dry Gin, belirgin ardýç ve narenciye notalarý.", ImageSource = "beefeaterlondondry.png", Price = 140.00, Rating = 4, Category = "Gin", TastingNotes = "Ardýç, limon kabuðu ve kiþniþ aromalarý. Damakta kuru ve baharatlý bir bitiþ.", AdditionalInfo = "Alkol Oraný: %40\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Tanqueray London Dry", Description = "Zengin ve baharatlý, klasik bir cin.", ImageSource = "tanqueraylondondry.png", Price = 155.00, Rating = 4, Category = "Gin", TastingNotes = "Ardýç, melekotu kökü ve meyan kökü aromalarý. Damakta yoðun ve kalýcý.", AdditionalInfo = "Alkol Oraný: %47.3\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Hendrick's Gin", Description = "Salatalýk ve gül yapraklarýyla zenginleþtirilmiþ özgün bir cin.", ImageSource = "hendricksgin.png", Price = 180.00, Rating = 4, Category = "Gin", TastingNotes = "Salatalýk, gül, ardýç ve narenciye aromalarý. Damakta yumuþak ve ferahlatýcý.", AdditionalInfo = "Alkol Oraný: %44\nÜretim Yeri: Ýskoçya" },
                new ProductModel { Name = "Bombay Sapphire", Description = "Egzotik baharatlar ve bitkilerle zenginleþtirilmiþ, dengeli bir cin.", ImageSource = "bombaysapphire.png", Price = 165.00, Rating = 4, Category = "Gin", TastingNotes = "Badem, limon kabuðu, meyan kökü ve zencefil aromalarý. Damakta yumuþak ve dengeli.", AdditionalInfo = "Alkol Oraný: %40\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "The Botanist Islay Dry Gin", Description = "Islay Adasý'ndan gelen 22 farklý bitki ile üretilen kompleks bir cin.", ImageSource = "thebotanist.png", Price = 220.00, Rating = 4, Category = "Gin", TastingNotes = "Yoðun bitkisel ve çiçeksi aromalar. Ardýç, nane, kiþniþ ve limon notalarý. Damakta zengin ve katmanlý.", AdditionalInfo = "Alkol Oraný: %46\nÜretim Yeri: Ýskoçya" },
                new ProductModel { Name = "Roku Gin", Description = "Japonya'dan gelen 6 özel bitki ile üretilen, zarif ve dengeli bir cin.", ImageSource = "rokugin.png", Price = 190.00, Rating = 4, Category = "Gin", TastingNotes = "Sakura çiçeði, yuzu kabuðu, sencha çayý, gyokuro çayý, sansho biberi ve ardýç aromalarý. Damakta taze ve çiçeksi.", AdditionalInfo = "Alkol Oraný: %43\nÜretim Yeri: Japonya" },
                new ProductModel { Name = "Monkey 47 Schwarzwald Dry Gin", Description = "47 farklý bitki ile üretilen, Almanya'nýn Kara Orman bölgesinden gelen kompleks bir cin.", ImageSource = "monkey47.png", Price = 350.00, Rating = 4, Category = "Gin", TastingNotes = "Yoðun baharat, çiçek ve meyve aromalarý. Ardýç, zencefil, lavanta ve greyfurt notalarý. Damakta zengin ve uzun.", AdditionalInfo = "Alkol Oraný: %47\nÜretim Yeri: Almanya" },
                new ProductModel { Name = "Nolet's Silver Dry Gin", Description = "Hollanda'dan gelen, gül, þeftali ve ahududu gibi meyvemsi ve çiçeksi notalara sahip modern bir cin.", ImageSource = "nolets.png", Price = 300.00, Rating = 4, Category = "Gin", TastingNotes = "Türk gülü, þeftali, ahududu ve ardýç aromalarý. Damakta yumuþak, meyvemsi ve çiçeksi.", AdditionalInfo = "Alkol Oraný: %47.6\nÜretim Yeri: Hollanda" },
                new ProductModel { Name = "Plymouth Gin", Description = "Ýngiltere'nin Plymouth þehrinden gelen, yumuþak ve dengeli bir cin.", ImageSource = "plymouth.png", Price = 150.00, Rating = 4, Category = "Gin", TastingNotes = "Ardýç, kiþniþ, portakal ve kakule aromalarý. Damakta yumuþak ve hafif tatlýmsý.", AdditionalInfo = "Alkol Oraný: %41.2\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Sipsmith London Dry Gin", Description = "Küçük partiler halinde bakýr imbiklerde üretilen, geleneksel bir London Dry Gin.", ImageSource = "sipsmith.png", Price = 175.00, Rating = 4, Category = "Gin", TastingNotes = "Ardýç, limon kabuðu, badem ve tarçýn aromalarý. Damakta kuru, baharatlý ve dengeli.", AdditionalInfo = "Alkol Oraný: %41.6\nÜretim Yeri: Ýngiltere" },
                new ProductModel { Name = "Aviation American Gin", Description = "Amerika'dan gelen, ardýçýn daha az baskýn olduðu, yumuþak ve baharatlý bir cin.", ImageSource = "aviation.png", Price = 160.00, Rating = 4, Category = "Gin", TastingNotes = "Lavanta, portakal kabuðu, kakule ve kiþniþ aromalarý. Damakta yumuþak ve baharatlý.", AdditionalInfo = "Alkol Oraný: %42\nÜretim Yeri: ABD" },
                new ProductModel { Name = "Citadelle Gin", Description = "Fransa'dan gelen, 19 farklý baharat ve bitki ile zenginleþtirilmiþ, zarif bir cin.", ImageSource = "citadelle.png", Price = 145.00, Rating = 4, Category = "Gin", TastingNotes = "Narenciye, baharat, çiçek ve ardýç aromalarý. Damakta dengeli ve ferahlatýcý.", AdditionalInfo = "Alkol Oraný: %44\nÜretim Yeri: Fransa" },
                new ProductModel { Name = "Absolut Original", Description = "Saf Ýsveç votkasý, pürüzsüz bir doku.", ImageSource = "absolutoriginal.png", Price = 90.00, Rating = 4, Category = "Vodka", TastingNotes = "Temiz ve nötr aroma profili, hafif tahýl notalarý.", AdditionalInfo = "Alkol Oraný: %40\nÜretim Yeri: Ýsveç" }
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
                ProductNameLabel.Text = "Ürün Bulunamadý";
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
            DisplayAlert("Barým", $"{_productName} barýna eklendi!", "OK");
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

                // Güncel ürünü bul ve Rating özelliðini güncelle
                var product = Products.FirstOrDefault(p => p.Name == _productName);
                if (product != null)
                {
                    product.Rating = _currentRating;
                    OnPropertyChanged(nameof(Products)); // Products koleksiyonunun deðiþtiðini bildir.
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