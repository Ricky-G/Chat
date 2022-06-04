

namespace Chat.Core.ViewModel;

public partial class HomeMoviesViewModel : BaseViewModel    
{
    [ObservableProperty]
    private List<Search> movies;

    private MovieService _movieService;

    public HomeMoviesViewModel(MovieService movieService)
    {
        _movieService = movieService;
        LoadMoviesAsync();
    }

    private async void LoadMoviesAsync()
    {
        var tempMovies = await _movieService.GetMoviesAsync();
            tempMovies.AddRange(await _movieService.GetMoviesAsync(2));
            tempMovies.AddRange(await _movieService.GetMoviesAsync(3));
            tempMovies.AddRange(await _movieService.GetMoviesAsync(4));

        Movies = tempMovies;
    }
}
