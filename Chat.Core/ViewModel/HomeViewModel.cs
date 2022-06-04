using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Core.ViewModel;

public partial class HomeViewModel : BaseViewModel  
{
    private HubConnection? hubConnection;
    [ObservableProperty]
    public List<MessageModel> messages = new();
    private List<MessageModel> temp = new();
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

        hubConnection.On<string, string>(nameof(Broadcast), (n, m) =>
        {
            temp.Insert(0, new MessageModel(n, m));
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

    public record MessageModel(string Name, string Message);
}
