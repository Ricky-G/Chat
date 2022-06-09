
namespace Chat.Mobile.View;

public partial class MoviesPage : ContentPage
{
	public MoviesPage(MoviesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}