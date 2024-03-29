﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

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
        try
        {

            _shell.Media.MediaPlayer.SetUriSource(new Uri(StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].PreviewUrl.AbsoluteUri));
            _shell.Media.MediaPlayer.Play();
            _shell._MediaPicture.ProfilePicture = new BitmapImage() { UriSource = new Uri(StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].Images[2].Url) };

            _shell._MediaName.Text = StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].Name;

            _shell.MediaArtist.Text = StaticDataBindingModel.TracksInAlbumsSpotify[TrackAlbums.SelectedIndex].Artists[0].Name;

        }
        catch (Exception)
        {

        }

    }


    private void HyperlinkButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {

        Artist data;
        TopAritisTrack topTrack;
        var btn = (TextBlock)sender;
        var n = (string)btn.Tag;

        try
        {
            data = JsonConvert.DeserializeObject<Artist>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService, $"https://api.spotify.com/v1/artists/{n}?market=VN").Content!)!;

            if (data != null)
            {
                StaticDataBindingModel.Artist = new();
                StaticDataBindingModel.Artist.Add(data);
            }
            topTrack = JsonConvert.DeserializeObject<TopAritisTrack>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService, $"https://api.spotify.com/v1/artists/{n}/top-tracks?market=VN").Content!)!;
            if (topTrack != null)
            {
                StaticDataBindingModel.TopSpotifyTracks = new();
                var index = 1;
                foreach (var item in topTrack.Tracks)
                {
                    item.Index = index;
                    StaticDataBindingModel.TopSpotifyTracks.Add(topTrack);
                    index++;
                }
            }
            ViewModel._navigationService.NavigateTo("Moon_Light_Music.ViewModels.NgheSiViewModel");
        }
        catch (Exception)
        {

        }

    }
}
