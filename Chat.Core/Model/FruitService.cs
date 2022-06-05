
using System.Text.Json;

namespace Chat.Core.Model;

public class FruitService 
{
    private readonly List<Fruit> _db = new List<Fruit>
        {
           new Fruit()
           {
               Source = "apple.png",
               Name = "Apple"
           },
           new Fruit()
           {
               Source = "bananas.png",
               Name = "Bananas"
           },
           new Fruit()
           {
               Source = "orange.png",
               Name = "Orange"
           },
           new Fruit()
           {
               Source = "guava.png",
               Name = "Guava"
           },
         new Fruit()
           {
               Source = "guava.png",
               Name = "Guava"
           },
           new Fruit()
            {
               Source = "watermelon.png",
               Name = "Watermelon"
           }
        };

    public Fruit GetFruit()
        => _db[new Random().Next(0, _db.Count - 1)];
}
