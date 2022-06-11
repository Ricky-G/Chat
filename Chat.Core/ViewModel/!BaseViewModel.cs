

namespace Chat.Core.ViewModel;

public partial class BaseViewModel : ObservableObject
{
   
    [ObservableProperty]
    bool isBusy;

    protected TelemetryClient telemetry => Globals.TelemetryInstance;
}
