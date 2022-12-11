using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;


public class TrangChuViewModel : ObservableRecipient
{
    public IOAuthTokkenService _oAuthTokkenService;


    public TrangChuViewModel(IOAuthTokkenService oAuthTokkenService)
    {
        _oAuthTokkenService = oAuthTokkenService;

        if (App._oAuthToken != null)
        {
            oAuthTokkenService.SetTokkenAsync(tokken: App._oAuthToken.Token);
        }
        else
        {
            App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Mất mạng rồi hay sao ấy bạn ơi🙄</text>
                            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                        </binding>
                    </visual>
                </toast>");
        }
    }

}
