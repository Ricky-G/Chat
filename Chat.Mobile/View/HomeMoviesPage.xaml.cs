
namespace Chat.Mobile.View;

public partial class HomeMoviesPage : ContentPage
{
	public HomeMoviesPage(HomeMoviesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}