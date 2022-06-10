
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
    private async void Login(object s)
    {
        await (s as Label).RotateTo(360, 500);
        await (s as Label).RotateTo(0, 500);
        await Task.Delay(100);
        Preferences.Set("Exception", "");
        Exception = "";
        await Shell.Current.GoToAsync("//Tabs/Home", true);
    }

}