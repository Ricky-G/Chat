

namespace Chat.Core.ViewModel;

public partial class MoviesViewModel : BaseViewModel    
{
    [ObservableProperty]
    private List<Search> movies;

    private MovieService _movieService;

    public MoviesViewModel(MovieService movieService)
    {
        _movieService = movieService;
        LoadMoviesAsync();
    }

    private async void LoadMoviesAsync()
    {
        try
        {
            var tempMovies = await _movieService.GetMoviesAsync().ConfigureAwait(false);
            /* tempMovies.AddRange(await _movieService.GetMoviesAsync(2));
             tempMovies.AddRange(await _movieService.GetMoviesAsync(3));
             tempMovies.AddRange(await _movieService.GetMoviesAsync(4));*/

            Movies = tempMovies;
        }
        catch(Exception e)
        {

        }
    }
}
