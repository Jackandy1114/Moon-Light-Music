using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

using Newtonsoft.Json;

namespace Moon_Light_Music.Views;

public sealed partial class BaiHatPage : Page
{
    public ShellPage Shell
    {
        get;
    }
    public BaiHatViewModel ViewModel
    {
        get;
    }
    public Button Btn_moreList => btn_moreList;
    public INavigationService _navigationService;
    public BaiHatPage()
    {
        ViewModel = App.GetService<BaiHatViewModel>();
        Shell = (ShellPage)App.MainWindow.Content;
        _navigationService = ViewModel._navigationService;
        InitializeComponent();
    }

    private void _borderTracks_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        try
        {
            //cần if
            //if (MediaSource.CreateFromUri(new Uri(StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri)) != _shell._media.MediaPlayer.Source)
            //{
            Shell.Media.MediaPlayer.SetUriSource(new Uri(StaticDataBindingModel.TracksSpotify[listviewAlbums.SelectedIndex].PreviewUrl.AbsoluteUri));
            Shell.Media.MediaPlayer.Play();
            /// đang làm việc ở đây

            Shell._MediaPicture.ProfilePicture = new BitmapImage() { UriSource = new Uri(StaticDataBindingModel.TracksSpotify[listviewAlbums.SelectedIndex].Album.Images[2].Url) };

            //_shell._song_img_url = StaticDataBindingModel._TracksSpotify[listviewAlbums.SelectedIndex].Images[2].Url;
            Shell._MediaName.Text = StaticDataBindingModel.TracksSpotify[listviewAlbums.SelectedIndex].Name;
            Shell.MediaArtist.Text = StaticDataBindingModel.TracksSpotify[listviewAlbums.SelectedIndex].Artists[0].Name;

            //}
        }
        catch (Exception)
        {

        }
    }

    private void Btn_moreList_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Tracks? _tracks = null;
        try
        {
            _tracks = JsonConvert.DeserializeObject<Tracks>(GetResponseFromAPIHelper.GetResponse(App._oAuthToken.Token, StaticDataBindingModel.RequestSpotifyTracks).Content)!;
        }
        catch (Exception)
        {

        }
        if (_tracks != null)
        {
            if (_tracks.Track.Items != null)
            {

                for (var i = 0; i < _tracks.Track.Items.Count!; i++)
                {
                    if (_tracks.Track.Items != null)
                    {
                        var track = _tracks.Track.Items[i];
                        StaticDataBindingModel.TracksSpotify.Add(track);
                    }
                }
                if (_tracks.Track.Next != null)
                {
                    StaticDataBindingModel.RequestSpotifyTracks = _tracks.Track.Next.ToString()!;
                }
                else
                {
                    btn_moreList.IsEnabled = false;
                }
            }
        }
        else
        {
            btn_moreList.IsEnabled = false;

        }
    }
}
