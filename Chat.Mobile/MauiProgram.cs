global using Chat.Core.Model;
global using Chat.Core.ViewModel;
global using Microsoft.ApplicationInsights;
global using Microsoft.ApplicationInsights.Extensibility;
global using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
global using Microsoft.Extensions.Configuration;
global using CommunityToolkit.Maui;
global using System.Reflection;
global using Chat.Mobile.View;

namespace Chat.Mobile;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "Default");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
              //  fonts.AddFont("junegull.ttf", "Default");
            });

        RegisterTypes(builder.Services);

        return builder.Build();
    }

    private static void RegisterTypes(IServiceCollection s)
    {
        s.AddSingleton(Core.Globals.GetTelemetryClient());
        s.AddSingleton<MovieService>();

        s.AddSingleton<LoginViewModel>();
        s.AddSingleton<LoginPage>();

        s.AddSingleton<HomeMoviesViewModel>();
        s.AddSingleton<HomeMoviesPage>();

        s.AddSingleton<ProfileViewModel>();
        s.AddSingleton<ProfilePage>();

        s.AddSingleton<HomeViewModel>();
        s.AddSingleton<HomePage>();

        s.AddSingleton<FruitService>();
        s.AddSingleton<FruitViewModel>();
        s.AddSingleton<FruitPage>();
    }

}

