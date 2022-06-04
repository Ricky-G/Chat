using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace Chat.Core.ViewModel;

public partial class HomeViewModel : BaseViewModel  
{
    private HubConnection? hubConnection;
    [ObservableProperty]
    public ObservableCollection<Message> messages = new();
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string message;
    public HomeViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://microsoft-chat.azurewebsites.net/chat")
            .Build();

        hubConnection.On<string, string>(nameof(Broadcast), (n,b) =>
        {
            Messages.Insert(0, new Message(n, b));
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
