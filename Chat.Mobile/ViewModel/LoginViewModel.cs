
namespace Chat.Core.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {

    }

    [ICommand]
    private async void Login(object s)
    {
        await Shell.Current.GoToAsync("//Tabs/Home", true);
    }


}