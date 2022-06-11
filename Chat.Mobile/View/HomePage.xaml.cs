namespace Chat.Mobile.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
		=> (BindingContext as HomeViewModel)?.Clear();
    
}