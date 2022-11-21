namespace Moon_Light_Music.Views.FrameViews;


public sealed class CustomMediaTransportControls : Microsoft.UI.Xaml.Controls.MediaTransportControls
{
    //public event EventHandler<EventArgs> Liked;

    public CustomMediaTransportControls()
    {
        DefaultStyleKey = typeof(CustomMediaTransportControls);
    }

    protected override void OnApplyTemplate()
    {
        // This is where you would get your custom button and create an event handler for its click method.
        //Button likeButton = GetTemplateChild("LikeButton") as Button;
        //likeButton.Click += LikeButton_Click;

        base.OnApplyTemplate();
    }

    //private void LikeButton_Click(object sender, RoutedEventArgs e)
    //{
    //    // Raise an event on the custom control when 'like' is clicked
    //    Liked?.Invoke(this, EventArgs.Empty);
    //}
}
