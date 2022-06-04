
using System.Text.Json;

namespace Chat.Core.Model;

public class FruitService 
{
    private readonly List<Fruit> _db = new List<Fruit>
        {
           new Fruit()
           {
               Index = "1",
               Source = "apple.png",
               Name = "Apple"
           },
           new Fruit()
           {
               Index = "2",
               Source = "bananas.png",
               Name = "Bananas"
           },
           new Fruit()
           {
               Index = "3",
               Source = "orange.png",
               Name = "Orange"
           },
           new Fruit()
           {
               Index = "4",
               Source = "guava.png",
               Name = "Guava"
           },
           new Fruit()
            {
               Index = "5",
               Source = "watermelon.png",
               Name = "Watermelon"
           }
        };

    public Fruit GetFruit()
        => _db[new Random().Next(0, _db.Count - 1)];
}
