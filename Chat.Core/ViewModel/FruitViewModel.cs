
using System.Collections.ObjectModel;

namespace Chat.Core.ViewModel;

public partial class FruitViewModel : BaseViewModel
{
    public ObservableCollection<Fruit> Fruits { get; set; } = new ObservableCollection<Fruit>();

    private readonly FruitService _fruitService;

    public FruitViewModel(FruitService fruitService)
    {
        _fruitService = fruitService;
    }

    [ICommand]
    public void Add()
    {
        Fruits.Insert(0, _fruitService.GetFruit());
    }

    [ICommand]
    public void Remove()
    {
        if (Fruits.Count == 0)
            return;
        Fruits.RemoveAt(Fruits.Count - 1);
    }
}
