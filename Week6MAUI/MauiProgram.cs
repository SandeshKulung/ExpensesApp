﻿using DataAccess.Services;
using DataAccess.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Week6MAUI
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
                });

            builder.Services.AddMauiBlazorWebView();

            //builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserAccess, UserAccess>();
            builder.Services.AddScoped<ICategory, CategoryService>();
            builder.Services.AddScoped<ITransaction, TransactionService>();
#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
