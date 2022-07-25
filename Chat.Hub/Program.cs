
using ChatCore;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Globals.GetTelemetryClient();

builder.Services.AddSingleton(Globals.TelemetryInstance);


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
