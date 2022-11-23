using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

using Newtonsoft.Json;

namespace Moon_Light_Music.Views;


public sealed partial class ChuDePage : Page
{
    public ChuDeViewModel ViewModel
    {
        get;
    }
    public INavigationService _navigationService;
    public ChuDePage()
    {
        ViewModel = App.GetService<ChuDeViewModel>();
        _navigationService = ViewModel._navigationService;
        InitializeComponent();
    }
    private async void _borderAlbums_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        Album? data = null;
        try
        {
            data = JsonConvert.DeserializeObject<Album>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService.OAuthTokken!, $"https://api.spotify.com/v1/albums/{StaticDataBindingModel.AlbumsSpotify[listviewAlbums.SelectedIndex].Id}?market=VN").Content!)!;
        }
        catch (Exception)
        {

        }
        if (data != null)
        {
            StaticDataBindingModel.TracksInAlbumsSpotify = new();
            StaticDataBindingModel._AlbumSpotify = new();
            StaticDataBindingModel._AlbumSpotify.Add(data);
            var data_artist = JsonConvert.DeserializeObject<Artist>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService.OAuthTokken!, $"https://api.spotify.com/v1/artists/{data.Artists[0].Id}").Content!)!;
            var index = 1;
            StaticDataBindingModel.Artist = new();
            StaticDataBindingModel.Artist.Add(data_artist);
            StaticDataBindingModel._AlbumSpotify[0].Artists[0] = data_artist;
            foreach (var item in data.Tracks.Items)
            {
                item.Index = index;
                item.Images ??= data.Images;
                StaticDataBindingModel.TracksInAlbumsSpotify.Add(item);
                index++;
            }
        }
        _navigationService.NavigateTo("Moon_Light_Music.ViewModels.ChuDeChildViewModel");
        await Task.CompletedTask.WaitAsync(new TimeSpan(ticks: 3000));
    }
}

