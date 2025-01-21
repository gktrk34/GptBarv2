using CommunityToolkit.Maui;
using GptBarv2.Data;
using GptBarv2.Models;
using GptBarv2.Repositories; // <-- EFProductRepository, IProductRepository
using Microsoft.EntityFrameworkCore;

namespace GptBarv2;

public static class MauiProgram
{
    // ServiceProvider'ı null! diyerek "ben biliyorum null olmaz" demiş oluyoruz.
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

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

        // EF Core ile DbContext
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydb.db3");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Filename={dbPath}");
        });

        // Repository’leri DI container’a ekleyelim
        builder.Services.AddScoped<IBrandRepository, EFBrandRepository>();
        builder.Services.AddScoped<IProductRepository, EFProductRepository>();

        // Uygulamayı inşa et
        var app = builder.Build();

        // Veritabanı oluşturma / seed
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated(); // Tablolar yoksa oluşturur
            SeedDatabase(db);
        }

        // ServiceProvider’ı saklayalım
        ServiceProvider = app.Services;

        return app;
    }

    private static void SeedDatabase(AppDbContext db)
    {
        if (!db.Brands.Any() && !db.Products.Any())
        {
            // ... (Aynı seed kodu)
            var brandGordons = new BrandModel
            {
                Name = "Gordon's",
                Category = "Gin",
                ImageSource = "gordons.png",
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

            var products = new List<ProductModel>
            {
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
                // ... Diğer ürünler
            };

            db.Brands.AddRange(brandGordons, brandBeefeater, brandAbsolut, brandGreyGoose);
            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}
