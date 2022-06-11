using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Core.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<Message> messages = new();
    private List<Message> temp = new();
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string message;
    private HubConnection hubConnection;
    public HomeViewModel()
    {
        SubscribeChatAsync();
    }

    private async void SubscribeChatAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://microsoft-chat.azurewebsites.net/chat")
            .Build();

        hubConnection.On<string, string>(nameof(Broadcast), (n, b) =>
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

    public void Clear()
    {
        temp.Clear();
        Messages = null;
    }

    public async ValueTask DisposeAsync()
    {
        await hubConnection.DisposeAsync();
    }
}
