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



    private void _borderAlbums_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        try
        {
            var a = JsonConvert.DeserializeObject<Album>(GetResponseFromAPIHelper.GetResponse(ViewModel._oAuthTokkenService.OAuthTokken, $"https://api.spotify.com/v1/albums/{StaticDataBindingModel.AlbumsSpotify[listviewAlbums.SelectedIndex].Id}?market=VN").Content!)!;
            if (a != null)
            {
                StaticDataBindingModel.TracksInAlbumsSpotify = new();
                StaticDataBindingModel._AlbumSpotify = new();
                StaticDataBindingModel._AlbumSpotify.Add(a);
                var index = 1;
                foreach (var item in a.Tracks.Items)
                {
                    item.Index = index;
                    item.Images ??= a.Images;

                    StaticDataBindingModel.TracksInAlbumsSpotify.Add(item);
                    index++;
                }
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            _navigationService.NavigateTo("Moon_Light_Music.ViewModels.ChuDeChildViewModel");
        }

    }
}

