using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class VideoPage : Page
{
    public VideoViewModel ViewModel
    {
        get;
    }

    public VideoPage()
    {
        ViewModel = App.GetService<VideoViewModel>();
        InitializeComponent();
    }
}
