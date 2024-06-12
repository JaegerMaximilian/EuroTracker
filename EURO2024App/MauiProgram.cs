using EURO2024App.Services;
using EURO2024App.View;
using EURO2024App.ViewModels;
using Microsoft.Extensions.Logging;

namespace EURO2024App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<EuroAPIService>();

            builder.Services.AddSingleton<SpieleViewModel>();
            builder.Services.AddSingleton<GamePage>();

            return builder.Build();
        }
    }
}
