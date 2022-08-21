﻿

namespace Chat.Core.ViewModel;

public partial class ProfileViewModel : BaseViewModel
{
    private bool _runThread;

    [RelayCommand]
    private async void LogOut()
        => await Shell.Current.GoToAsync($"//Login");

    [RelayCommand]
    private void Unhandled()
        => int.Parse("invalid");

    [RelayCommand]
    public async void StartMonitor()
    {
        _runThread = true;

        List<Thread> threads = new();
        for (int i = 0; i < 100000; i++)
        {
            threads.Add(new Thread(new ThreadStart(KillCore)));
        }

        _runThread = false;

        for (int i = 0; i < 20; i++)
        {
            Telemetry.TrackDependency(
                "Bad Dependency",
                "target",
                "data",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(50 * i),
                true);

            Telemetry.TrackDependency(
                "Good Dependency",
                "target",
                "data",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(3 * i),
                true);

            Telemetry.TrackRequest("Bad Request",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(50 * i),
                "200",
                true);
            Telemetry.TrackRequest("Bad Request",
                DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(3 * i),
                "200",
                true);

            await Task.Delay(100);
        }

        try { int.Parse("invalid"); }
        catch (Exception e) { Telemetry.TrackException(e); }
        try { int.Parse("invalid"); }
        catch (Exception e) { Telemetry.TrackException(e); }
        try {
            Fruit f = new(null,null);
            string upper = f.Name.ToUpper();
        }
        catch (Exception e)
        {
            Telemetry.TrackException(e);
        }
    }
    private void KillCore()
    {
        long num = 0;
        while (_runThread)
        {
            num += new Random().Next(100, 1000);
            if (num > 1000000) { num = 0; }
        }
    }

}
