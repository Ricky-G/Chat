
namespace Chat.Core.Test;

public class IntegrationTest
{
    [Fact]
    public void FruitServiceIntegrationTest()
    {
        FruitService fruitService = new();
        List<Fruit> fruits = new();

        // Add 3 fruits
        fruits.Add(fruitService.GetFruit());
        fruits.Add(fruitService.GetFruit());
        fruits.Add(fruitService.GetFruit());

        Assert.True(fruits.Count == 3);
    }
}