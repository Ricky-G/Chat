
namespace Chat.Core.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string username;

    [ICommand]
    private async void Login()
    {
       await Shell.Current.GoToAsync("//Tabs");
    }

}