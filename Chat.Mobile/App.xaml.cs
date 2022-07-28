namespace Chat.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new Root();
        Shell.Current.GoToAsync("//Login");
    }

    private void Current_PageAppearing(object sender, Page e)
    {
        Console.WriteLine($"Page Appearing: {e.GetType()}");
    }

}