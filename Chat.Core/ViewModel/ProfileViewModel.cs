

namespace Chat.Core.ViewModel;

public partial class ProfileViewModel : BaseViewModel
{
    private bool _runThread;
    private TelemetryClient _telemetry;

    public ProfileViewModel(TelemetryClient telemetry)
    {
        _telemetry = telemetry;
    }

    [ICommand]
    public async void LogOut()
    {
        await Shell.Current.GoToAsync($"//Login");
    }

    [ICommand]
    private async void StartMonitor()
    {
        _runThread = true;

        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < 100000; i++)
        {
            threads.Add(new Thread(new ThreadStart(KillCore)));
        }

        _runThread = false;

        for (int i = 0; i < 20; i++)
        {
            _telemetry.TrackDependency(
                "Bad Dependency",
                "target",
                "data",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(50 * i),
                true);

            _telemetry.TrackDependency(
                "Good Dependency",
                "target",
                "data",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(3 * i),
                true);

            _telemetry.TrackRequest("Bad Request",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(50 * i),
                "200",
                true);

            _telemetry.TrackRequest("Bad Request",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(3 * i),
                "200",
                true);

            await Task.Delay(100);
        }

        try
        {
            int.Parse("invalid");
        }
        catch (Exception e)
        {
            _telemetry.TrackException(e);
        }
    }
    private void KillCore()
    {
        Random rand = new Random();
        long num = 0;
        while (_runThread)
        {
            num += rand.Next(100, 1000);
            if (num > 1000000) { num = 0; }
        }
    }

}
