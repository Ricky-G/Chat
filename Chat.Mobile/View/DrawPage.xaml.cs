using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace Chat.Mobile.View;

public partial class DrawPage : ContentPage
{
	public DrawPage(DrawViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        var gestureImage = new Image();
        var drawingView = new DrawingView
        {
            Lines = new ObservableCollection<IDrawingLine>(),
            DrawingLineCompletedCommand = new Command<IDrawingLine>(async (line) =>
            {
                var stream = await line.GetImageStream(gestureImage.Width, gestureImage.Height, Colors.Gray.AsPaint());
                gestureImage.Source = ImageSource.FromStream(() => stream);
            })
        };
        drawingView.DrawingLineCompleted += async (s, e) =>
        {
            var stream = await e.LastDrawingLine.GetImageStream(gestureImage.Width, gestureImage.Height, Colors.Gray.AsPaint());
            gestureImage.Source = ImageSource.FromStream(() => stream);
        };
    }
}