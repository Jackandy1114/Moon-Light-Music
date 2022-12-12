using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;
using Moon_Light_Music.Services;

namespace Moon_Light_Music.ViewModels;

public class TaiKhoanViewModel : ObservableRecipient
{
    public string AccountPicture
    => StaticDataBindingModel.AccountPicture;
    public string AccountEmail => StaticDataBindingModel.AccountEmail;
    public string AccountName => StaticDataBindingModel.AccountName;
    public IisLoginService _isLoginService;
    public INavigationService _navigationService;
    public TaiKhoanViewModel(INavigationService navigationService, IisLoginService isLoginService)
    {
    
        _navigationService = navigationService;
        _isLoginService = isLoginService;
    }
}
