using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.Services;

using Newtonsoft.Json;

namespace Moon_Light_Music.ViewModels;

public class ChuDeViewModel : ObservableRecipient
{
    private static bool First = false;
    public IOAuthTokkenService _oAuthTokkenService;
    public INavigationService _navigationService;
    //public IAppNotificationService _appNotification;
    public bool IsEnableBtnMoreLoading
    => StaticDataBindingModel._isEnableBtn_moreLoading;
    public ObservableCollection<Item> AlbumsSpotify => StaticDataBindingModel.AlbumsSpotify;

    public ICommand moreListcommand;
    public ChuDeViewModel(IOAuthTokkenService oAuthTokkenService, INavigationService navigationService)
    {
        _navigationService = navigationService;
        _oAuthTokkenService = oAuthTokkenService;
        if (!First)
        {
            AlbumsCollection(_oAuthTokkenService.OAuthTokken!).WaitAsync(new TimeSpan(3000));
            First = true;
        }
        moreListcommand = new RelayCommand(async () =>
        {
            await AlbumsCollection(_oAuthTokkenService.OAuthTokken!).ConfigureAwait(false);
        }
       );
    }

    public static async Task AlbumsCollection(string OAuth2Tokken)
    {
        if (StaticDataBindingModel._isEnableBtn_moreLoading)
        {
            Albums? _albums;

            if (StaticDataBindingModel.RequestSpotifyALbums == "")
            {
                StaticDataBindingModel.RequestSpotifyALbums = "https://api.spotify.com/v1/browse/new-releases?country=VN&limit=10&offset=0";
            }
            try
            {
                _albums = JsonConvert.DeserializeObject<Albums>(GetResponseFromAPIHelper.GetResponse(OAuth2Tokken, StaticDataBindingModel.RequestSpotifyALbums).Content!)!;
                if (_albums != null)
                {
                    if (_albums.Album.Items != null)
                    {

                        for (var i = 1; i < _albums.Album.Items.Count; i++)
                        {
                            if (_albums.Album.Items != null)
                            {
                                var track = _albums.Album.Items[i];
                                StaticDataBindingModel.AlbumsSpotify.Add(track);
                            }
                        }
                        if (_albums.Album.Next != null)
                        {
                            StaticDataBindingModel.RequestSpotifyALbums = _albums.Album.Next.ToString()!;
                        }
                        else
                        {
                            StaticDataBindingModel._isEnableBtn_moreLoading = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //var token = JsonConvert.DeserializeObject<GetAPI>(GetResponseFromAPIHelper.GetResponse_AndToken().Content!)!;
               
                               
            }

        }
        else
        {
            //Chổ này có thể set vào thẳng view luôn : | tự dưng làm rườm rà zậy Hoàng(Bản thể A của Đạt)
            StaticDataBindingModel._isEnableBtn_moreLoading = false;
        }
        await Task.CompletedTask.WaitAsync(new TimeSpan(ticks: 3000));
    }

}

