namespace Chat.Mobile;

public partial class App : Application
{
    public static TelemetryClient Telemetry { get; private set; }
    public App(TelemetryClient telemetryClient)
    {
        Telemetry = telemetryClient;
        Telemetry.Context.User.Id = Guid.NewGuid().ToString();

        InitializeComponent();

        MainPage = new Root();
        Shell.Current.GoToAsync("//Login");
    }
}