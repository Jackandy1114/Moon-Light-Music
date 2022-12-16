using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class NgheSiPage : Page
{
    public ShellPage _shell
    {
        get;
    }
    public NgheSiViewModel ViewModel
    {
        get;
    }

    public NgheSiPage()
    {
        ViewModel = App.GetService<NgheSiViewModel>();

        _shell = (ShellPage)App.MainWindow.Content;
        InitializeComponent();
    }

    private void SymbolIcon_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        try
        {

            _shell.Media.MediaPlayer.SetUriSource(new Uri(StaticDataBindingModel.TopSpotifyTracks[0].Tracks[TrackAlbums.SelectedIndex].PreviewUrl.AbsoluteUri));
            _shell.Media.MediaPlayer.Play();
            _shell._MediaPicture.ProfilePicture = new BitmapImage() { UriSource = new Uri(StaticDataBindingModel.TopSpotifyTracks[0].Tracks[TrackAlbums.SelectedIndex].Album.Images[2].Url) };

            _shell._MediaName.Text = StaticDataBindingModel.TopSpotifyTracks[0].Tracks[TrackAlbums.SelectedIndex].Name;

            _shell.MediaArtist.Text = StaticDataBindingModel.TopSpotifyTracks[0].Tracks[TrackAlbums.SelectedIndex].Artists[0].Name;

        }
        catch (Exception)
        {

        }
    }
}
