using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();
builder.Host.ConfigureLogging((context, b) =>
{
    b.AddApplicationInsights("InstrumentationKey=e14a93ed-fdf0-45db-8aec-5b20a4fd0fad;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/");
    b.AddFilter<ApplicationInsightsLoggerProvider>( typeof(Program).FullName, LogLevel.Trace); }
);
    ApplicationInsightsServiceOptions o = new ();
    o.EnableAdaptiveSampling = false;
    o.EnableQuickPulseMetricStream = true;
    builder.Services.AddApplicationInsightsTelemetry(o);
    builder.Services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) =>
    {
        module.AuthenticationApiKey = "8hxec7lfgznbtb2jgeotb4hnrkbf087jnmwpejgl";
    });


var app = builder.Build();
    app.MapHub<Chat>(nameof(Chat));
    app.MapGet("/", () => "Hello Index");
    app.MapGet("/null", () => {
        throw new NullReferenceException();
        });
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();

    public class Chat : Hub
    {
        public async void Broadcast(string name, string message)
        {

            try
            {
                string data = null;

                if (message == "null")
                    data.ToString();

                await Clients.All.SendAsync(nameof(Broadcast), name, message);
            }
            catch (Exception e)
            {

            }
        }

    }