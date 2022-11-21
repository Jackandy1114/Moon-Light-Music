using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class BaiHatPage : Page
{
    public ShellPage _shell
    {
        get;
    }
    public BaiHatViewModel ViewModel
    {
        get;
    }
    public INavigationService _navigationService;
    public BaiHatPage()
    {
        ViewModel = App.GetService<BaiHatViewModel>();
        _shell = (ShellPage)App.MainWindow.Content;
        _navigationService = ViewModel._navigationService;
        InitializeComponent();

    }

    private void _borderTracks_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        try
        {
            if (new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri) != StaticDataBindingModel._PLayingMedia)
            {
                StaticDataBindingModel._PLayingMedia = new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri);
                _shell._media.MediaPlayer.SetUriSource(new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri));
                _shell._media.MediaPlayer.Play();

            }
        }
        catch (Exception)
        {

        }
    }
}
