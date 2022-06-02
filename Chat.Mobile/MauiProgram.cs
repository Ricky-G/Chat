global using Chat.Model;
global using Chat.ViewModel;
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
        var s = a.GetManifestResourceStream($"{a.GetName().Name}.appsettings.json");

        var config = new ConfigurationBuilder().AddJsonStream(s).Build();
        TelemetrySettings tmstgs = config.GetRequiredSection("Settings").Get<TelemetrySettings>();

        builder.Configuration.AddConfiguration(config);

        RegisterTypes(builder.Services, tmstgs);

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

        s.AddSingleton<HomeViewModel>();
        s.AddSingleton<HomePage>();
    }

    private static TelemetryClient GetTelemetryClient(TelemetrySettings settings)
    {
        TelemetryConfiguration cfg = TelemetryConfiguration.CreateDefault();
        cfg.ConnectionString = settings.AppInsights;
        QuickPulseTelemetryProcessor qp = null;
        cfg.DefaultTelemetrySink.TelemetryProcessorChainBuilder
            .Use((next) =>
            {
                qp = new QuickPulseTelemetryProcessor(next);
                return qp;
            })
            .Build();

        var qpm = new QuickPulseTelemetryModule
        {
            AuthenticationApiKey = settings.QuickPulse
        };
        qpm.Initialize(cfg);
        qpm.RegisterTelemetryProcessor(qp);
        TelemetryClient client = new(cfg);
        return client;
    }
}

public class TelemetrySettings
{
    public string AppInsights { get; set; }
    public string QuickPulse { get; set; }
}
