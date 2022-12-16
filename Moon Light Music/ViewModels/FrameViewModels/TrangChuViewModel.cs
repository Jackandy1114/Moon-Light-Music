using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;

using Newtonsoft.Json;

namespace Moon_Light_Music.ViewModels;


public class TrangChuViewModel : ObservableRecipient
{
    public IOAuthTokkenService _oAuthTokkenService;


    public TrangChuViewModel(IOAuthTokkenService oAuthTokkenService)
    {
        _oAuthTokkenService = oAuthTokkenService;

        if (_oAuthTokkenService.OAuthTokken == "null")
        {
            try
            {
                var token = JsonConvert.DeserializeObject<GetAPI>(GetResponseFromAPIHelper.GetResponse_AndToken().Content!)!;
                _oAuthTokkenService.SetTokkenAsync(token.Token);
            }
            catch (Exception)
            {
                App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Mất mạng rồi hay sao ấy bạn ơi🙄</text>
                           </binding>
                    </visual>
                </toast>");
            }
        }

    }

}
