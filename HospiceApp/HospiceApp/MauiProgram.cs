using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using HospiceApp.ViewModels;
using HospiceApp.Views;
using LoginTestAppMaui.Services.Abstract;
using LoginTestAppMaui.Services.Implementation;

namespace HospiceApp;

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
        
        builder.Services.AddSingleton<IStrapiService, StrapiService>();

        builder.Services.AddTransient<MainPage, MainViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}