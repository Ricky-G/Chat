
namespace Chat.Core.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string exception;

    public LoginViewModel()
    {
        Exception = Preferences.Get("Exception", "");
        if (!string.IsNullOrEmpty(Exception))
        {
            Exception = $"Send to Monitor => {Exception}";
        }
    }

    [ICommand]
    private async void Login(TelemetryClient telemetry)
    {
       await Shell.Current.GoToAsync("//Tabs/Home");
       Preferences.Set("Exception", "");
       Exception = "";
    }

}