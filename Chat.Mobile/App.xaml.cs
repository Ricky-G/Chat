namespace Chat.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new Root();
        Shell.Current.GoToAsync("//Login");
    }

    protected override void OnStart()
    {
        base.OnStart();

       // App.Current.PageAppearing += Current_PageAppearing;
    }

    private void Current_PageAppearing(object sender, Page e)
    {
        Console.WriteLine($"Page Appearing: {e.GetType()}");
    }
}