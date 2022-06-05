
global using Microsoft.ApplicationInsights;
global using Chat.Core.Model;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Microsoft.ApplicationInsights.Extensibility;
global using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
global using System.Reflection;
global using Microsoft.Extensions.Configuration;
global using System.Globalization;
using Microsoft.ApplicationInsights.DataContracts;

namespace Chat.Core;

public static class Globals
{
    public static TelemetryClient GetTelemetryClient()
    {
        var settings = Settings();
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

        SetDeviceProperties(client.Context);
        RegisterUnhandledExceptions();

        return client;
    }

    private static void SetDeviceProperties(TelemetryContext context)
    {
        context.Device.Model ??= DeviceInfo.Model;
        context.Device.OperatingSystem ??= DeviceInfo.Platform.ToString();
        context.Device.Language ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        // client.Context.Device.ScreenResolution ??= DeviceDisplay.MainDisplayInfo.ToString();
        context.Device.OemName ??= DeviceInfo.Current.Manufacturer.ToString();
        context.Device.Type ??= $"{DeviceInfo.Current.DeviceType} {DeviceInfo.Current.Idiom}";
        context.Device.NetworkType ??= Connectivity.Current.NetworkAccess.ToString();

        string uniqueID = Guid.NewGuid().ToString();

        context.User.AccountId ??= uniqueID;
        context.User.Id ??= uniqueID;
    }

    private static void RegisterUnhandledExceptions()
    {
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Task.Run(async () =>
            {
              /*  System.Diagnostics.Debug.WriteLine("AppDomain.CurrentDomain.UnhandledException: {0}. IsTerminating: {1}", e.ExceptionObject, e.IsTerminating);
                await Task.Delay(1000).ConfigureAwait(false);
                Telemetry.TrackException(e.ExceptionObject as Exception);
                await Task.Delay(4000).ConfigureAwait(false);*/
            }).Wait();
        };
    }

    private static TelemetrySettings Settings()
    {
        var a = typeof(Globals).GetTypeInfo().Assembly;
        var s = a.GetManifestResourceStream($"{a.GetName().Name}.appsettings.json");

        var config = new ConfigurationBuilder().AddJsonStream(s).Build();
        TelemetrySettings settings = config.GetRequiredSection(nameof(Settings)).Get<TelemetrySettings>();
        return settings;
    }
}