namespace Chat.Mobile.View;

public partial class StreamPage : ContentPage
{
	public StreamPage()
	{
		InitializeComponent();
	}

	private async void Forward(object sender, EventArgs e)
	{
         await web.EvaluateJavaScriptAsync($"forward()");
    }

    private async void Back(object sender, EventArgs e)
    {
        await web.EvaluateJavaScriptAsync($"back()");
    }
}