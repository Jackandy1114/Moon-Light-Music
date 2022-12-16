using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;
public class ChuDeChildViewModel : ObservableRecipient
{
    public IOAuthTokkenService _oAuthTokkenService;
    public INavigationService _navigationService;
    public ObservableCollection<Item> TracksInAlbumsSpotify => StaticDataBindingModel.TracksInAlbumsSpotify;
    public ObservableCollection<Album> _AlbumSpotify => StaticDataBindingModel.AlbumSpotify;
    public ObservableCollection<Artist> Artist => StaticDataBindingModel.Artist;

    public ChuDeChildViewModel(IOAuthTokkenService oAuthTokkenService, INavigationService navigationService)
    {
        _oAuthTokkenService = oAuthTokkenService;
        _navigationService = navigationService;
    }
}
