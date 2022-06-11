

namespace Chat.Core.ViewModel;

public partial class MoviesViewModel : BaseViewModel    
{
    [ObservableProperty]
    private List<Search> movies;

    private MovieService _movieService;

    public MoviesViewModel(MovieService movieService)
    {
        _movieService = movieService;
    }

    [ICommand]
    private async void SearchMovies(object s)
    {
        try
        {
            var tempMovies = await _movieService.GetMoviesAsync(s as string).ConfigureAwait(false);
            Movies = tempMovies;
        }
        catch(Exception e)
        {
            telemetry.TrackException(e);
        }
    }
}
