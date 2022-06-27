
using ChatCore;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(Globals.GetTelemetryClient());

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
app.UseSwagger();
app.UseSwaggerUI();
app.Run();

public class Chat : Hub
{
    public async void Broadcast(string name, string message)
    {
        await Clients.All.SendAsync(nameof(Broadcast), name, message);
    }

}