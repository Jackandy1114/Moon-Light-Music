using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;
public class TaiKhoanSignUpChildViewModel : ObservableRecipient
{
    public INavigationService _navigationService;
    public TaiKhoanSignUpChildViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
