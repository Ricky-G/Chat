using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSignalR(); 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.MapHub<Chat>(nameof(Chat));
    app.MapGet("/", ()=> "Hello Index");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();

public class Chat : Hub
{
    public void Broadcast(string name, string message)
        => Clients.All.SendAsync(nameof(Broadcast), name, message);
    
}