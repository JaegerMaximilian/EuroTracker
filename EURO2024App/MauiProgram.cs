﻿using EURO2024App.Services;
using EURO2024App.View;
using EURO2024App.ViewModels;
using Microsoft.Extensions.Logging;
using EURO2024App.Services;

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

            builder.Services.AddSingleton<GamesViewModel>();
            builder.Services.AddTransient<EventViewModel>();
            builder.Services.AddTransient<AddEventViewModel>();

            builder.Services.AddSingleton<GamesPage>();
            builder.Services.AddTransient<EventPage>();
            builder.Services.AddTransient<AddEventPage>();

            builder.Services.AddSingleton<GruppenViewModel>();
            builder.Services.AddSingleton<GruppenPage>();

            builder.Services.AddSingleton<StatistikViewModel>();
            builder.Services.AddTransient<StatistikPage>();

            return builder.Build();
        }
       
    }
    
}
