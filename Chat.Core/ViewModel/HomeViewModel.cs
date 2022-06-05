using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace Chat.Core.ViewModel;

public partial class HomeViewModel : BaseViewModel  
{
    private HubConnection? hubConnection;
    [ObservableProperty]
    private List<Message> messages = new();
    private List<Message> temp = new();
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string message;
    public HomeViewModel()
    {
        SubscribeChatAsync();
    }

    private async void SubscribeChatAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://microsoft-chat.azurewebsites.net/chat")
            .Build();

        hubConnection.On<string, string>(nameof(Broadcast), (n,b) =>
        {
            temp.Insert(0, new Message(n, b));
            Messages = null;
            Messages = temp;
    
        });

        await hubConnection.StartAsync();
    }

    [ICommand]
    public async void Broadcast()
    {
        await hubConnection.SendAsync(nameof(Broadcast), Name, Message);
        Message = null;
    }

    public async ValueTask DisposeAsync()
    {
        await hubConnection.DisposeAsync();
    }
}
