using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;
public class TaiKhoanLoginViewModel : ObservableRecipient
{
    public INavigationService _navigationService;
    public TaiKhoanLoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
