namespace Chat.Mobile.View;

public partial class FruitPage : ContentPage
{
	public FruitPage(FruitViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}