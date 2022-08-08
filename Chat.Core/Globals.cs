
global using System.Text.Json;
global using System.Collections.ObjectModel;
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

namespace ChatCore;

public static class Globals
{
    public static TelemetryClient TelemetryInstance;

    public static TelemetryClient GetTelemetryClient()
    {
        if (TelemetryInstance != null)
            return TelemetryInstance;

        TelemetrySettings settings = Settings();
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

        string uniqueID = Guid.NewGuid().ToString();

        client.Context.User.AccountId ??= uniqueID;
        client.Context.User.Id ??= uniqueID;

        TelemetryInstance = client;
        return TelemetryInstance;
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