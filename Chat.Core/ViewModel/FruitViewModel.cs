
using System.Collections.ObjectModel;

namespace Chat.Core.ViewModel;

public partial class FruitViewModel : BaseViewModel
{
    public ObservableCollection<Fruit> Fruits { get; set; } = new ObservableCollection<Fruit>();

    private readonly FruitService _fruitService;
    private readonly TelemetryClient _telemetryClient;

    public FruitViewModel(FruitService fruitService, TelemetryClient telemetryClient)
    {
        _fruitService = fruitService;
        _telemetryClient = telemetryClient;
    }

    [ICommand]
    public void Add()
    {
        Fruit fruit = _fruitService.GetFruit();
        _telemetryClient.TrackEvent(fruit.Name);
        Fruits.Insert(0, fruit);
    }

    [ICommand]
    public void Remove()
    {
        if (Fruits.Count == 0)
            return;
        Fruits.RemoveAt(Fruits.Count - 1);
    }
}
