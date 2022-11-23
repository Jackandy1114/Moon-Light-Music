using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;

using Newtonsoft.Json;

namespace Moon_Light_Music.ViewModels;

public class ChuDeViewModel : ObservableRecipient
{
    public static bool first = false;
    public IOAuthTokkenService _oAuthTokkenService;
    public INavigationService _navigationService;
    public IAppNotificationService _appNotification;
    public bool IsEnableBtn_moreLoading
    => StaticDataBindingModel._isEnableBtn_moreLoading;
    public ObservableCollection<Item> AlbumsSpotify => StaticDataBindingModel.AlbumsSpotify;

    public ICommand moreListcommand;
    public ChuDeViewModel(IOAuthTokkenService oAuthTokkenService, INavigationService navigationService, IAppNotificationService appNotification)
    {
        _appNotification = appNotification;
        _navigationService = navigationService;
        _oAuthTokkenService = oAuthTokkenService;
        if (!first)
        {
            AlbumsCollection(oAuthTokkenService.OAuthTokken!, oAuthTokkenService);
            first = true;
        }
        moreListcommand = new RelayCommand(async () =>
        {
            await AlbumsCollection(oAuthTokkenService.OAuthTokken!, oAuthTokkenService).ConfigureAwait(false);
        }
       );
    }

    public static async Task AlbumsCollection(string OAuth2Tokken, IOAuthTokkenService _oAuthTokkenService)
    {
        if (StaticDataBindingModel._isEnableBtn_moreLoading)
        {
            Albums _albums;

            if (StaticDataBindingModel.RequestSpotifyALbums == "")
            {
                StaticDataBindingModel.RequestSpotifyALbums = "https://api.spotify.com/v1/browse/new-releases?country=VN&limit=10&offset=0";
            }
            try
            {
                _albums = JsonConvert.DeserializeObject<Albums>(GetResponseFromAPIHelper.GetResponse(OAuth2Tokken, StaticDataBindingModel.RequestSpotifyALbums).Content!)!;
                if (_albums != null)
                {
                    for (var i = 1; i < _albums.Album.Items.Count; i++)
                    {
                        if (_albums.Album.Items != null)
                        {
                            var track = _albums.Album.Items[i];
                            StaticDataBindingModel.AlbumsSpotify.Add(track);
                        }
                    }
                    if (_albums.Album.Next != null)
                    {
                        StaticDataBindingModel.RequestSpotifyALbums = _albums.Album.Next.ToString()!;
                    }
                    else
                    {
                        StaticDataBindingModel._isEnableBtn_moreLoading = false;
                    }
                }
            }
            catch (Exception)
            {
                //                ContentDialog _dialog = new ContentDialog()
                //                {
                //                    XamlRoot = App.MainWindow.Content.XamlRoot,
                //                    Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                //                    Title = "Nhập tokken mới",
                //                    PrimaryButtonText = "Lưu",
                //                    SecondaryButtonText = "Lấy tự động",
                //                    CloseButtonText = "Rời đi",
                //                    DefaultButton = ContentDialogButton.Primary,
                //                };
                //                _dialog.Content = new TrangChuContentDiaglog();
                //                ContentDialogResult result = await _dialog.ShowAsync();
                //                if (result == ContentDialogResult.Primary)
                //                {
                //                    var _content = (TrangChuContentDiaglog)_dialog.Content;
                //                    await _oAuthTokkenService.SetTokkenAsync(_content.New_OAuthor2Tokken);
                //                }
                //                else if (result == ContentDialogResult.Secondary)
                //                {

                //                    _appNotification.Show(@"<toast>
                //    <visual>
                //        <binding template=""ToastGeneric"">
                //            <text hint-maxLines=""1"">Trần Hoàng</text>
                //            <text>❤️Chờ mình lấy tokken xíu nha😊</text>
                //            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                //        </binding>
                //    </visual>
                //</toast>");
                //                    await _oAuthTokkenService.SetTokkenAsync(await GetResponseFromAPIHelper.getTokkenOnlineBySelenium());
                //                    _appNotification.Show(@"<toast>
                //    <visual>
                //        <binding template=""ToastGeneric"">
                //            <text hint-maxLines=""1"">Trần Hoàng</text>
                //            <text>❤️Lấy tokken thành công😊</text>
                //            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                //        </binding>
                //    </visual>
                //</toast>");
                //                    StaticDataBindingModel.AlbumsSpotify = new();
                //                    _navigationService.NavigateTo("Moon_Light_Music.ViewModels.TrangChuViewModel");
                //                    _navigationService.NavigateTo("Moon_Light_Music.ViewModels.ChuDeViewModel");
                //                }
            }

        }
        else
        {
            //Chổ này có thể set vào thẳng view luôn : | tự dưng làm rườm rà zậy Hoàng(Bản thể A của Đạt)
            StaticDataBindingModel._isEnableBtn_moreLoading = false;
        }
        await Task.CompletedTask.WaitAsync(new TimeSpan(ticks: 3000));
    }

}

