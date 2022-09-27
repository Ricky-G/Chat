
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace Chat.Core.ViewModel;

public partial class FruitViewModel : BaseViewModel, IAsyncDisposable
{
    public ObservableCollection<Fruit> Fruits { get; set; } = new ObservableCollection<Fruit>();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Fruits))]
    int rot = 2;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Fruits))]
    int size = 100;
    private readonly FruitService _fruitService;
    private readonly TelemetryClient _telemetryClient;
    private HubConnection hub;

    public FruitViewModel(FruitService fruitService, TelemetryClient telemetryClient)
    {
        _fruitService = fruitService;
        _telemetryClient = telemetryClient;
        LoadAsync();
    }

    private async void LoadAsync()
    {
        try
        {
            hub = new HubConnectionBuilder()
                        .WithUrl("https://alfarahn-chat.azurewebsites.net/chat")
                        .Build();

            hub.On<string, string>("SendFruit", (source, name) =>
            {
                if (source == "column")
                {
                    Rot = int.Parse(name);
                    return;
                }
                else if (source == "size")
                {
                    Size = int.Parse(name);

                    return;
                }

                Fruit fruit = new(source, name);
#if !DEBUG
            _telemetryClient.TrackEvent(fruit.Name);
#endif
                Fruits.Insert(0, fruit);

            });

            await hub.StartAsync();
        }
        catch(Exception e)
        {
            Telemetry.TrackException(e);
        }
    }

    [RelayCommand]
    public void Add()
    {
        Fruit fruit = _fruitService.GetFruit();
        _telemetryClient.TrackEvent(fruit.Name);
        Fruits.Insert(0, fruit);
    }

    [RelayCommand]
    public void Remove()
    {
        if (Fruits.Count == 0)
            return;
        Fruits.RemoveAt(Fruits.Count - 1);
    }

    public async ValueTask DisposeAsync()
    {
        await hub.DisposeAsync();
    }
}
