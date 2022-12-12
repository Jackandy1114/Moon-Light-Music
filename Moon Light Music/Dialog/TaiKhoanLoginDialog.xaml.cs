using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;

namespace Moon_Light_Music.Dialog;

public sealed partial class TaiKhoanLoginDialog : ContentDialog
{
    public TaiKhoanLoginDialog()
    {
        InitializeComponent();
    }

    private void RevealModeCheckbox_Changed(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //if (revealModeCheckBox.IsChecked == true)
        //{
        //    passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Visible;
        //}
        //else
        //{
        //    passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Hidden;
        //}

    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        //    using var db = new MoonLightMusicDataBaseContext();
        //    try
        //    {
        //        var query = db.UserAccounts.Where(p => p.Email == tb_Email.Text).FirstOrDefault();
        //        if (query == null || query.Password != passworBoxWithRevealmode.Password)
        //        {
        //            App.GetService<IAppNotificationService>().Show(@"<toast>
        //                <visual>
        //                    <binding template=""ToastGeneric"">
        //                        <text hint-maxLines=""1"">Trần Hoàng</text>
        //                        <text>❤️Đăng nhập thất bại🤨</text>
        //                        <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
        //                    </binding>
        //                </visual>
        //            </toast>");
        //        }
        //        else
        //        {
        //            App.GetService<IAppNotificationService>().Show(@"<toast>
        //                <visual>
        //                    <binding template=""ToastGeneric"">
        //                        <text hint-maxLines=""1"">Trần Hoàng</text>
        //                        <text>❤️Đăng nhập thành công😊</text>
        //                        <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
        //                    </binding>
        //                </visual>
        //            </toast>");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        App.GetService<IAppNotificationService>().Show(@"<toast>
        //                <visual>
        //                    <binding template=""ToastGeneric"">
        //                        <text hint-maxLines=""1"">Trần Hoàng</text>
        //                        <text>❤️Đã có lỗi xảy ra🥲</text>
        //                        <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
        //                    </binding>
        //                </visual>
        //            </toast>");
        //        //await App.MainWindow.ShowMessageDialogAsync("Đăng nhập thất bại", "Thất bại");
        //    }
        
    }
}