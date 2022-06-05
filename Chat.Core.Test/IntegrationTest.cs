
namespace Chat.Core.Test;

public class IntegrationTest
{
    [Fact]
    public void FruitServiceTest()
    {
        FruitService fruitService = new();
        List<Fruit> fruits = new();

        // Add 3 fruits
        fruits.Add(fruitService.GetFruit());
        fruits.Add(fruitService.GetFruit());
        fruits.Add(fruitService.GetFruit());

        Assert.True(fruits.Count == 3);
    }

    [Fact]
    public async Task MovieServiceTest()
    {
        MovieService movieService = new();
        List<Search> list = await movieService.GetMoviesAsync();

        Assert.True(list.Count > 0);
    }
}