using Chat.Blazor;
using Chat.Core.ViewModel;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

//builder.Services.AddSingleton(Chat.Core.Globals.GetTelemetryClient());
builder.Services.AddSingleton<HomeViewModel>();

await builder.Build().RunAsync();
