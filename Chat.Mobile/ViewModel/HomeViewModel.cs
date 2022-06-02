using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace Chat.ViewModel;

public partial class HomeViewModel : BaseViewModel  
{
    private HubConnection? hubConnection;
    [ObservableProperty]
    public List<MessageModel> messages = new();
    private List<MessageModel> temp = new();
    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private string? message;
    public HomeViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://microsoft-chat.azurewebsites.net/chat")
            .Build();

        hubConnection.On<string, string>(nameof(Broadcast), (n, m) =>
        {
            temp.Add(new MessageModel(n, m));
            Messages = null;
            Messages = temp;
        });

        await hubConnection.StartAsync();
    }

    [ICommand]
    private async void Broadcast()
    {
        await hubConnection.SendAsync(nameof(Broadcast), name, message);
        Message = null;
    }

    public async ValueTask DisposeAsync()
    {
        await hubConnection.DisposeAsync();
    }

    public record MessageModel(string N, string B);
}
