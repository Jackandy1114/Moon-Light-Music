using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;


public class TrangChuViewModel : ObservableRecipient
{
    public IOAuthTokkenService _oAuthTokkenService;


    public TrangChuViewModel(IOAuthTokkenService oAuthTokkenService)
    {
        _oAuthTokkenService = oAuthTokkenService;
        oAuthTokkenService.SetTokkenAsync(tokken: App._oAuthToken.Token);
    }

}
