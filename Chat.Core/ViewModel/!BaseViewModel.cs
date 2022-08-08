

using ChatCore;

namespace Chat.Core.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
   
    [ObservableProperty]
    bool isBusy;

    public TelemetryClient Telemetry => Globals.TelemetryInstance;

    public virtual void OnAppearing() { }
    public virtual void OnDisappearing() { }
}
