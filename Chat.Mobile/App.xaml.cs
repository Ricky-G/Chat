namespace Chat.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new Root();
        Shell.Current.GoToAsync("//Login");
    }

    protected override void OnStart()
    {
        base.OnStart();

        // App.Current.PageAppearing += Current_PageAppearing;
        SendUnhandledExceptions();

    }

    private void Current_PageAppearing(object sender, Page e)
    {
        Console.WriteLine($"Page Appearing: {e.GetType()}");
    }

    private void SendUnhandledExceptions()
    {
        if (Preferences.Get("Exception", "") is string exception &&
            !string.IsNullOrEmpty(exception))
        {

            ChatCore.Globals.TelemetryInstance.TrackException(new UnhandledException(exception));
            Preferences.Set("Exception", "");
        }
    }

    public class UnhandledException : Exception {
        public UnhandledException(string e) : base(e)
        {

        }
    }
}