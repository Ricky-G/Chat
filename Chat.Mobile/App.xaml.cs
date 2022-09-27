namespace Chat.Mobile;

public partial class App : Application, IDisposable
{
    public App()
    {
        InitializeComponent();

        PageAppearing += App_PageAppearing;
        PageDisappearing += App_PageDisappearing;

        MainPage = new Root();

        Shell.Current.GoToAsync("//Login");

    }

    private void App_PageDisappearing(object sender, Page e)
    {
        if (e.BindingContext is BaseViewModel vm)
        {
            vm.OnDisappearing();
        }
    }

    private void App_PageAppearing(object sender, Page e)
    {
        if (e.BindingContext is BaseViewModel vm)
        {
            vm.OnAppearing();
            Globals.TelemetryInstance.TrackPageView(e.GetType().Name);
        }
        else
        {
            var p = e.GetType().Name;

            if (!p.Contains("Page"))
                return;

            var vmString = $"{p.Substring(0, p.Length - 4)}ViewModel";

            var type = this.GetType().Assembly.GetType($"Chat.Core.ViewModel.{vmString}");

            var ser = MauiProgram.Services.GetServices<object>();

            var context = MauiProgram.Services.GetService(type);
            e.BindingContext = context;

        }
    }

    public void Dispose()
    {
        PageAppearing -= App_PageAppearing;
        PageDisappearing -= App_PageDisappearing;
    }

}