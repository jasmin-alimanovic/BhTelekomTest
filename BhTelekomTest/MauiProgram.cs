﻿

using BhTelekomTest.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BhTelekomTest
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

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .Build();

            // Register configuration
            builder.Configuration.AddConfiguration(configuration);
#endif
            builder.Services.AddAppServices(configuration);


            return builder.Build();
        }
    }
}
