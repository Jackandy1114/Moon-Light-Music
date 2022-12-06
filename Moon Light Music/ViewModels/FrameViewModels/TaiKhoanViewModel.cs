using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;

public class TaiKhoanViewModel : ObservableRecipient
{
    public string AccountPicture
    => StaticDataBindingModel.AccountPicture;
    public string AccountEmail => StaticDataBindingModel.AccountEmail;
    public string AccountName => StaticDataBindingModel.AccountName;
    public INavigationService _navigationService;
    public TaiKhoanViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
