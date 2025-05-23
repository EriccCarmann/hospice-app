﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using HospiceApp.ViewModels;
using HospiceApp.Views;
using HospiceApp.Services.Abstract;
using HospiceApp.Services.Implementation;
using zoft.MauiExtensions.Controls;

namespace HospiceApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseZoftAutoCompleteEntry()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<IStrapiService, StrapiService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        
        builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
        builder.Services.AddTransientWithShellRoute<AllDiseasesPage, AllDiseasesViewModel>(nameof(AllDiseasesPage));
        builder.Services.AddTransientWithShellRoute<SearchDiseasesPage, SearchDiseasesViewModel>(nameof(SearchDiseasesPage));

        builder.Services.AddTransientPopup<AddOrEditDiseasePopup, AddOrEditDiseasePopupViewModel>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}