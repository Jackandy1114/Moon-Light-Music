using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

using Newtonsoft.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ChuDeChildPage : Page
{
    public ShellPage _shell
    {
        get;
    }

    public ChuDeChildViewModel ViewModel
    {
        get;
    }
    public ChuDeChildPage()
    {
        ViewModel = App.GetService<ChuDeChildViewModel>();
        _shell = (ShellPage)App.MainWindow.Content;

        InitializeComponent();
        //_shell = App.GetService<ShellPage>();
    }

    private void SymbolIcon_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {

        if (new Uri(StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].PreviewUrl.AbsoluteUri) != StaticDataBindingModel._PLayingMedia)
        {
            StaticDataBindingModel._PLayingMedia = new Uri(StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].PreviewUrl.AbsoluteUri);
            _shell._media.MediaPlayer.SetUriSource(new Uri(StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].PreviewUrl.AbsoluteUri));
            _shell._media.MediaPlayer.Play();
        }
    }


    private void HyperlinkButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        Artist _artist;
        try
        {
            _artist = JsonConvert.DeserializeObject<Artist>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService.OAuthTokken!, @$"https://api.spotify.com/v1/artists/{StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].Artists[0].Id}").Content!)!;

            if (_artist != null)
            {

            }
        }
        catch (Exception)
        {

            throw;
        }

    }
}
