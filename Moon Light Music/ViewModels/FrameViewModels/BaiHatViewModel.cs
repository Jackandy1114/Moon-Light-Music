using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;

public class BaiHatViewModel : ObservableRecipient
{
    public ObservableCollection<Item> TracksSpotify => StaticDataBindingModel._TracksSpotify;
    public INavigationService _navigationService;
    public BaiHatViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
