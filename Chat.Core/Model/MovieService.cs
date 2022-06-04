
using System.Text.Json;

namespace Chat.Core.Model;

public class MovieService
{
    public async Task<List<Search>> GetMoviesAsync(int page = 1)
    {
        HttpClient client = new();
        string url = $"https://www.omdbapi.com/?s=game&apikey=245934a4&page={page}";
        string json = await client.GetStringAsync(url);
        MovieSearch search = JsonSerializer.Deserialize<MovieSearch>(json);
        return search.Search;  
    }
}
