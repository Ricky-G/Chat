using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSignalR(); 

var app = builder.Build();
    app.MapHub<Chat>(nameof(Chat));
    app.Run();

public class Chat : Hub
{
    public void Broadcast(string name, string message)
        => Clients.All.SendAsync(nameof(Broadcast), name, message);
    
}