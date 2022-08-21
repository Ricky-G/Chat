using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Core.ViewModel;

public partial class HomeViewModel : BaseViewModel, IAsyncDisposable
{
    [ObservableProperty]
    private List<Message> messages = new();
    private List<Message> temp = new();
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string message;
    private HubConnection hub;
    public HomeViewModel()
    {
        SubscribeChatAsync();
    }

    private async void SubscribeChatAsync()
    {
        try
        {
            hub = new HubConnectionBuilder()
                .WithUrl("https://alfarahn-chat.azurewebsites.net/chat")
                .Build();

            hub.On<string, string>(nameof(Broadcast), (n, b) =>
            {
                temp.Insert(0, new Message(n, b));
                Messages = null;
                Messages = temp;

            });

            await hub.StartAsync();
        }
        catch(Exception e)
        {
            Telemetry.TrackException(e);
        }
    }

    [RelayCommand]
    public async void Broadcast()
    {
        await hub.SendAsync(nameof(Broadcast), Name, Message);
        Message = null;
    }

    public void Clear()
    {
        temp.Clear();
        Messages = null;
    }

    public async ValueTask DisposeAsync()
    {
        await hub.DisposeAsync();
    }
}
