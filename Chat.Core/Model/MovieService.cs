
namespace Chat.Core.Model;

public class MovieService
{
    public async Task<List<Search>> GetMoviesAsync(string keyword, int page = 1)
    {
        HttpClient client = new();
        string url = $"https://www.omdbapi.com/?s={keyword}&apikey=245934a4&page={page}";
        string json = await client.GetStringAsync(url);
        MovieSearch res = JsonSerializer.Deserialize<MovieSearch>(json);
        return res.Search;  
    }
}
