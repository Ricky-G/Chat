
using ChatCore;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(Globals.GetTelemetryClient());

/*

var aiOptions = new ApplicationInsightsServiceOptions();
// Disables adaptive sampling.
aiOptions.EnableAdaptiveSampling = false;
// Disables QuickPulse (Live Metrics stream).
aiOptions.EnableQuickPulseMetricStream = true;
aiOptions.ConnectionString = "InstrumentationKey=e14a93ed-fdf0-45db-8aec-5b20a4fd0fad;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/";
builder.Services.AddApplicationInsightsTelemetry(aiOptions);
builder.Services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) => module.AuthenticationApiKey = "8hxec7lfgznbtb2jgeotb4hnrkbf087jnmwpejgl");
*/

var app = builder.Build();
app.MapHub<Chat>(nameof(Chat));
app.MapGet("/", () => "Hello Index");
app.MapGet("/null", () =>
{
    var telemetry = app.Services.GetService<TelemetryClient>();
    try
    {
        string data = null;
        data.ToString();
    }
    catch (Exception e)
    {
        telemetry.TrackException(e);
    }
});

app.MapGet("helloali",()=> "hello form new API");

app.UseSwagger();
app.UseSwaggerUI();
app.Run();

public class Chat : Hub
{
    public async void Broadcast(string name, string message)
    {
        await Clients.All.SendAsync(nameof(Broadcast), name, message);
    }
    public async void SendFruit(string source, string name)
    {
        await Clients.All.SendAsync(nameof(SendFruit), source, name);
    }
}
