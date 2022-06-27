

using ChatCore;

namespace Chat.Core.ViewModel;

public partial class BaseViewModel : ObservableObject
{
   
    [ObservableProperty]
    bool isBusy;

    public TelemetryClient Telemetry => Globals.TelemetryInstance;
}
