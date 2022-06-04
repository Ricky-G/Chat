namespace Chat.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new Root();
        Shell.Current.GoToAsync("//Login");
    }
}