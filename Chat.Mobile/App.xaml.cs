﻿namespace Chat.Mobile;

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
    }

    public void Dispose()
    {
        PageAppearing -= App_PageAppearing;
        PageDisappearing -= App_PageDisappearing;
    }

}