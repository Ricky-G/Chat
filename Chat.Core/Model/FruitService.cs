namespace Chat.Core.Model;

public class FruitService
{
    private readonly List<Fruit> _db = new()
    {
        new Fruit("apple.png","Apple"),
        new Fruit("bananas.png","Bananas"),
        new Fruit("orange.png","Orange"),
        new Fruit("guava.png","Guava"),
        new Fruit("guava.png","Guava"),
        new Fruit("guava.png","Guava"),
        new Fruit("watermelon.png", "Watermelon")
    };

    public Fruit GetFruit()
        => _db[new Random().Next(0, _db.Count - 1)];
}
