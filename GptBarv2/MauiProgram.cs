using CommunityToolkit.Maui;
using GptBarv2.Data;
using GptBarv2.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GptBarv2;

public static class MauiProgram
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

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

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydb.db3");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Filename={dbPath}");
        });

        builder.Services.AddScoped<IBrandRepository, EFBrandRepository>();
        builder.Services.AddScoped<IProductRepository, EFProductRepository>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            SeedDatabase(db);
        }

        ServiceProvider = app.Services;
        return app;
    }

    private static void SeedDatabase(AppDbContext db)
    {
        if (!db.Brands.Any() && !db.Products.Any())
        {
            // Seed işlemleri...
        }
    }
}
