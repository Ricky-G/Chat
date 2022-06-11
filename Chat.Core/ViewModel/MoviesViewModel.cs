

namespace Chat.Core.ViewModel;

public partial class MoviesViewModel : BaseViewModel    
{
    [ObservableProperty]
    private List<Search> movies;
    [ObservableProperty]
    private int page;
    [ObservableProperty]
    private string search;

    private MovieService _movieService;

    public MoviesViewModel(MovieService movieService)
    {
        _movieService = movieService;
    }

    [ICommand]
    private async void SearchMovies()
    {
        try
        {
            Movies = await _movieService.GetMoviesAsync(Search, Page);
        }
        catch(Exception e)
        {
            Telemetry.TrackException(e);
        }
    }
}
