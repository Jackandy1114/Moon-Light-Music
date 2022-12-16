using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;
public class TaiKhoanSignUpChildViewModel : ObservableRecipient
{
    public INavigationService _navigationService;
    public IisLoginService _isLoginService;
    public List<string> listEmail = new List<string>();
    public TaiKhoanSignUpChildViewModel(INavigationService navigationService, IisLoginService isLoginService)
    {
        _navigationService = navigationService;
        _isLoginService = isLoginService;
        listEmail.Add(_isLoginService.account);
    }
}
