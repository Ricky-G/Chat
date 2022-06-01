global using Chat.Model;
global using Chat.Mobile.ViewModel;
global using Microsoft.ApplicationInsights;
global using Microsoft.ApplicationInsights.Extensibility;
global using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
global using Microsoft.Extensions.Configuration;
global using CommunityToolkit.Maui;
global using System.Reflection;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
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
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("junegull.ttf", "JuneGull");
            });

        var a = typeof(App).GetTypeInfo().Assembly;
        var stream = a.GetManifestResourceStream($"{a.GetName().Name}.appsettings.json");

        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
        TelemetrySettings telemetryAppSettings = config.GetRequiredSection("Settings").Get<TelemetrySettings>();

        builder.Configuration.AddConfiguration(config);

        RegisterTypes(builder.Services, telemetryAppSettings);

        return builder.Build();
    }

    private static void RegisterTypes(IServiceCollection s, TelemetrySettings telemetryAppSettings)
    {
        s.AddSingleton(GetTelemetryClient(telemetryAppSettings));
        s.AddSingleton<MovieService>();

        s.AddSingleton<LoginViewModel>();
        s.AddSingleton<LoginPage>();

        s.AddSingleton<HomeMoviesViewModel>();
        s.AddSingleton<HomeMoviesPage>();

        s.AddSingleton<ProfileViewModel>();
        s.AddSingleton<ProfilePage>();
    }

    private static TelemetryClient GetTelemetryClient(TelemetrySettings settings)
    {
        TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
        config.ConnectionString = settings.AppInsights;
        QuickPulseTelemetryProcessor quickPulseProcessor = null;
        config.DefaultTelemetrySink.TelemetryProcessorChainBuilder
            .Use((next) =>
            {
                quickPulseProcessor = new QuickPulseTelemetryProcessor(next);
                return quickPulseProcessor;
            })
            .Build();

        var quickPulseModule = new QuickPulseTelemetryModule
        {
            AuthenticationApiKey = settings.QuickPulse
        };
        quickPulseModule.Initialize(config);
        quickPulseModule.RegisterTelemetryProcessor(quickPulseProcessor);
        TelemetryClient client = new(config);
        return client;
    }
}

public class TelemetrySettings
{
    public string AppInsights { get; set; }
    public string QuickPulse { get; set; }
}
