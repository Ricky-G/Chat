
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
using System.Globalization;

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

      
        client.Context.Device.Model ??= DeviceInfo.Model;
        client.Context.Device.OperatingSystem ??= DeviceInfo.Platform.ToString();
        client.Context.Device.Language ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        client.Context.Device.ScreenResolution ??= DeviceDisplay.MainDisplayInfo.ToString();
        
        string uniqueID = Guid.NewGuid().ToString();
        client.Context.User.AccountId ??= uniqueID;
        client.Context.User.Id ??= uniqueID;

        return client;
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