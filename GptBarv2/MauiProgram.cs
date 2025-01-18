using CommunityToolkit.Maui;
using GptBarv2.Data;
using GptBarv2.Repositories; // Birazdan ekleyeceğiz
using Microsoft.EntityFrameworkCore;
using GptBarv2.Models;

namespace GptBarv2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // EF Core Sqlite
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydb.db3");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Filename={dbPath}");
        });

        // Repository eklersek (BrandRepository, vs.)
        builder.Services.AddScoped<IBrandRepository, EFBrandRepository>();

        var app = builder.Build();

        // DB oluştur, seed veriler ekle
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            SeedDatabase(db);
        }
        builder.Services.AddScoped<IProductRepository, EFProductRepository>();

        return app;
    }

    private static void SeedDatabase(AppDbContext db)
    {
        // Tablolar boşsa eklemeye başlayalım
        if (!db.Brands.Any() && !db.Products.Any())
        {
            // 1) MARKALAR (BrandModel)
            var brandGordons = new BrandModel
            {
                Name = "Gordon's",
                Category = "Gin",
                ImageSource = "gordons.png", // Marka logosu
                                             // Products listesini boş bırakıyoruz; aşağıda eklenecek
            };

            var brandBeefeater = new BrandModel
            {
                Name = "Beefeater",
                Category = "Gin",
                ImageSource = "beefeater.png",
            };

            var brandAbsolut = new BrandModel
            {
                Name = "Absolut",
                Category = "Vodka",
                ImageSource = "absolut.png",
            };

            var brandGreyGoose = new BrandModel
            {
                Name = "Grey Goose",
                Category = "Vodka",
                ImageSource = "greygoose.png",
            };

            // Daha fazla marka ekleyebilirsiniz.
            // Örneğin brandTanqueray, brandHendricks, vs.

            // 2) ÜRÜNLER (ProductModel)
            // Her ürün, "Brand" navigation property’si üzerinden markayla ilişkilendirilecek.
            var products = new List<ProductModel>
        {
            // Gordons
            new ProductModel
            {
                Name = "Gordon's London Dry",
                Description = "Klasik London Dry gin; yoğun bitkisel aromalar.",
                ImageSource = "gordonslondondry.png",
                Price = 120.00,
                Rating = 4,
                Category = "Gin",
                TastingNotes = "Yoğun ardıç, kişniş ve melekotu aromaları. Damakta narenciye ve baharatlı notalar.",
                AdditionalInfo = "Alkol Oranı: %43\nÜretim Yeri: İngiltere",
                Brand = brandGordons
            },
            new ProductModel
            {
                Name = "Gordon's Premium Pink",
                Description = "Meyvemsi notalar ve hafif çiçeksi aromalar.",
                ImageSource = "gordonspremiumpink.png",
                Price = 135.00,
                Rating = 4,
                Category = "Gin",
                TastingNotes = "Çilek, ahududu ve frenk üzümü aromaları. Hafif tatlı ve ferahlatıcı.",
                AdditionalInfo = "Alkol Oranı: %37.5\nÜretim Yeri: İngiltere",
                Brand = brandGordons
            },

            // Beefeater
            new ProductModel
            {
                Name = "Beefeater London Dry",
                Description = "Geleneksel bir London Dry Gin, belirgin ardıç ve narenciye notaları.",
                ImageSource = "beefeaterlondondry.png",
                Price = 140.00,
                Rating = 4,
                Category = "Gin",
                TastingNotes = "Ardıç, limon kabuğu ve kişniş aromaları. Damakta kuru ve baharatlı bir bitiş.",
                AdditionalInfo = "Alkol Oranı: %40\nÜretim Yeri: İngiltere",
                Brand = brandBeefeater
            },

            // Absolut
            new ProductModel
            {
                Name = "Absolut Original",
                Description = "Saf İsveç votkası, pürüzsüz bir doku.",
                ImageSource = "absolutoriginal.png",
                Price = 90.00,
                Rating = 4,
                Category = "Vodka",
                TastingNotes = "Temiz ve nötr aroma profili, hafif tahıl notaları.",
                AdditionalInfo = "Alkol Oranı: %40\nÜretim Yeri: İsveç",
                Brand = brandAbsolut
            },
            new ProductModel
            {
                Name = "Absolut Citron",
                Description = "Limon ve narenciye dokusuyla ferahlatıcı bir votka.",
                ImageSource = "absolutcitron.png",
                Price = 95.00,
                Rating = 4,
                Category = "Vodka",
                TastingNotes = "Turunçgil, limon kabuğu, hafif ekşi ve taze aromalar.",
                AdditionalInfo = "Alkol Oranı: %40\nÜretim Yeri: İsveç",
                Brand = brandAbsolut
            },

            // Grey Goose
            new ProductModel
            {
                Name = "Grey Goose Original",
                Description = "Fransız lüks votkası, buğday bazlı.",
                ImageSource = "greygooseoriginal.png",
                Price = 190.00,
                Rating = 5,
                Category = "Vodka",
                TastingNotes = "Buğday bazlı yumuşak tat, hafif fındıksı aromalar.",
                AdditionalInfo = "Alkol Oranı: %40\nÜretim Yeri: Fransa",
                Brand = brandGreyGoose
            },
        };

            // 3) Veritabanına ekleyelim
            // Önce markaları ekleyelim
            db.Brands.AddRange(brandGordons, brandBeefeater, brandAbsolut, brandGreyGoose);

            // Sonra ürünleri
            db.Products.AddRange(products);

            db.SaveChanges();
        }
    }
}
    
