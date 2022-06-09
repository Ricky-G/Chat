
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
    private async void Login()
    {
        Preferences.Set("Exception", "");
        Exception = "";
        await Shell.Current.GoToAsync("//Tabs/Home");
      /*  await Task.Delay(1500);
        await Shell.Current.GoToAsync("//Tabs/Fruit");
        await Task.Delay(1500);
        await Shell.Current.GoToAsync("//Tabs/Profile");
        await Task.Delay(1500);
        await Shell.Current.GoToAsync("//Tabs/Home");*/
    }

}