using Chat.Blazor;
using Chat.Core.ViewModel;
using ChatCore;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

var aiOptions = new ApplicationInsightsServiceOptions();
// Disables adaptive sampling.
aiOptions.EnableAdaptiveSampling = false;
// Disables QuickPulse (Live Metrics stream).
aiOptions.EnableQuickPulseMetricStream = true;
aiOptions.ConnectionString = "InstrumentationKey=e14a93ed-fdf0-45db-8aec-5b20a4fd0fad;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/";
builder.Services.AddApplicationInsightsTelemetry(aiOptions);
builder.Services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) => module.AuthenticationApiKey = "8hxec7lfgznbtb2jgeotb4hnrkbf087jnmwpejgl");

builder.Services.AddSingleton(Globals.GetTelemetryClient());
builder.Services.AddSingleton<HomeViewModel>();

await builder.Build().RunAsync();
