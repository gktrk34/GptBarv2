using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GptBarv2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>() // App.xaml.cs içindeki 'App' sınıfını kullanır
                .ConfigureFonts(fonts =>
                {
                    // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    // Buraya font ekleyebilirsin
                });

            // Eğer Dependency Injection ekleyeceğin servisler varsa burada kaydedersin:
            // builder.Services.AddSingleton<IFavoritesService, FavoritesServiceMock>();

            return builder.Build();
        }
    }
}
