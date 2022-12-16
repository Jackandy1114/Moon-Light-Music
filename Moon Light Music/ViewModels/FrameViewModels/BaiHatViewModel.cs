using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;

public class BaiHatViewModel : ObservableRecipient
{
    public ObservableCollection<Item> TracksSpotify => StaticDataBindingModel.TracksSpotify;
    public INavigationService _navigationService;
    public IOAuthTokkenService _oAuthTokkenService;
    public BaiHatViewModel(INavigationService navigationService, IOAuthTokkenService oauthTokkenService)
    {
        _navigationService = navigationService;
        _oAuthTokkenService = oauthTokkenService;
    }
}
