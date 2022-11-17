using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

using Windows.Media.Core;

namespace Moon_Light_Music.Views;

public sealed partial class BaiHatPage : Page
{
    public BaiHatViewModel ViewModel
    {
        get;
    }
    public INavigationService _navigationService;
    public BaiHatPage()
    {
        ViewModel = App.GetService<BaiHatViewModel>();
        _navigationService = ViewModel._navigationService;
        InitializeComponent();

    }

    private async void _borderTracks_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        try
        {
            if (new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri) != StaticDataBindingModel._PLayingMedia)
            {
                StaticDataBindingModel._PLayingMedia = new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri);
                await Task.Run(() => StaticDataBindingModel.mediaPlayer.Source = MediaSource.CreateFromUri(StaticDataBindingModel._PLayingMedia));
                StaticDataBindingModel.mediaPlayer.Play();
            }
        }
        catch (Exception)
        {

        }
    }
}
