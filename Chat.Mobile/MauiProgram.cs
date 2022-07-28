global using Chat.Core.Model;
global using Chat.Core.ViewModel;
global using Microsoft.ApplicationInsights;
global using Microsoft.ApplicationInsights.Extensibility;
global using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
global using Microsoft.Extensions.Configuration;
global using CommunityToolkit.Maui;
global using System.Reflection;
global using Chat.Mobile.View;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;
global using System.Threading;
global using ChatCore;
using System.Globalization;
using Microsoft.ApplicationInsights.DataContracts;

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
             //   fonts.AddFont("OpenSans-Regular.ttf", "Default");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("junegull.ttf", "Default");
            });

        RegisterTypes(builder.Services);

        return builder.Build();
    }


    private static void RegisterTypes(IServiceCollection s)
    {
        s.AddSingleton(s.BuildServiceProvider());

        var t = Globals.GetTelemetryClient();
            t.Context.SetDeviceProperties();

        RegisterUnhandledExceptions();
        SendUnhandledException();

        s.AddSingleton(t);
        s.AddSingleton<MovieService>();

        s.AddSingleton<LoginViewModel>();
        s.AddSingleton<LoginPage>();

        s.AddSingleton<MoviesViewModel>();
        s.AddSingleton<MoviesPage>();

        s.AddSingleton<ProfileViewModel>();
        s.AddSingleton<ProfilePage>();

        s.AddSingleton<HomeViewModel>();
        s.AddSingleton<HomePage>();

        s.AddSingleton<FruitService>();
        s.AddSingleton<FruitViewModel>();
        s.AddSingleton<FruitPage>();

        s.AddSingleton<DrawViewModel>();
        s.AddSingleton<DrawPage>();
    }

    public static void SetDeviceProperties(this TelemetryContext context)
    {
        context.Device.Model ??= DeviceInfo.Model;
        context.Device.OperatingSystem ??= DeviceInfo.Platform.ToString();
        context.Device.Language ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        // client.Context.Device.ScreenResolution ??= DeviceDisplay.MainDisplayInfo.ToString();
        context.Device.OemName ??= DeviceInfo.Current.Manufacturer.ToString();
        context.Device.Type ??= $"{DeviceInfo.Current.DeviceType} {DeviceInfo.Current.Idiom}";
        context.Device.NetworkType ??= Connectivity.Current.NetworkAccess.ToString();
    }

    private static void RegisterUnhandledExceptions()
    {
        // This is a hack, purpose is only to show it is possible
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {

            string page = Shell.Current?.CurrentPage?.GetType().Name;
            string viewmodel = Shell.Current?.CurrentPage?.BindingContext?.GetType().Name;

            Preferences.Set("Exception", $"{page} {viewmodel} {(e.ExceptionObject as Exception).Message} {(e.ExceptionObject as Exception)?.InnerException} {(e.ExceptionObject as Exception)}");
        };

        AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
        {
            if (args.Exception.Message.Contains("canceled") ||
                args.Exception.Message.Contains("supported") ||
                args.Exception.Message.Contains("Failed to perform") ||
                !string.IsNullOrEmpty(Preferences.Get("Exception", "")))
                return;

            string page = Shell.Current.CurrentPage?.GetType().Name;
            string viewmodel = Shell.Current.CurrentPage?.BindingContext?.GetType().Name;
            Preferences.Set("Exception", $"{page} {viewmodel} {args.Exception.Message} {args.Exception?.InnerException} {args.Exception}");
        };
    }


    private static void SendUnhandledException()
    {
        Task.Run(async () => { 
        if (Preferences.Get("Exception", "") is string exception &&
            !string.IsNullOrEmpty(exception))
        {
            await Task.Delay(3000);
            ChatCore.Globals.TelemetryInstance.TrackException(new UnhandledException(exception));
            Preferences.Set("Exception", "");
        }
        });
    }

    public class UnhandledException : Exception
    {
        public UnhandledException(string e) : base(e)
        {

        }
    }
}

