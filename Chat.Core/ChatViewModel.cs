
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace Chat.Core;

public partial class ChatViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> messages = new();
    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private string? message;

    private HubConnection? _hubConnection;
    public ChatViewModel()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://microsoft-chat.azurewebsites.net/chat")
            .Build();

        _hubConnection.On<string, string>(nameof(Broadcast), (name, message) =>
        {
            var encodedMsg = $"{name} {message}";
            Messages.Add(encodedMsg);
        });

        _hubConnection.StartAsync();
    }

    [ICommand]
    public async void Broadcast()
    {
        if (_hubConnection == null)
            return;

        await _hubConnection.SendAsync(nameof(Broadcast), Name, Message);
        Message = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection == null)
            return;

        await _hubConnection.DisposeAsync();
    }
}
