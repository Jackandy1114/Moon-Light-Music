// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TaiKhoanSignUpChildPage : Page
{
    private string id = "User1";
    public TaiKhoanSignUpChildViewModel ViewModel
    {
        get;
    }
    public TaiKhoanSignUpChildPage()
    {
        ViewModel = App.GetService<TaiKhoanSignUpChildViewModel>();
        this.InitializeComponent();
    }

    private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var cat in ViewModel.listEmail)
            {
                var found = splitText.All((key) =>
                {
                    return cat.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(cat);
                }
            }
            if (suitableItems.Count == 0)
            {
                //suitableItems.Add("Không tìm thấy bài hát");

                //suitableItems.Add("No results found");

            }
            sender.ItemsSource = suitableItems;
        }

    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        using var db = new MoonLightMusicDataBaseContext();
        try
        {
            var query = db.UserAccounts.Where(p => p.Email == tb_Email.Text).FirstOrDefault();
            if (query == null || query.Password != passworBoxWithRevealmode.Password)
            {
                App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Đăng nhập thất bại🤨</text>
                            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                        </binding>
                    </visual>
                </toast>");
            }
            else
            {
                id = query.UserProfile;
                StaticDataBindingModel.AccountEmail = query.Email;
                App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Đăng nhập thành công😊</text>
                            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                        </binding>
                    </visual>
                </toast>");
                if ((bool)SavePassword.IsChecked)
                {
                    await ViewModel._isLoginService.SetTokkenAsync("true", _password: query.Password, _account: query.Email);
                }
                else
                {
                    await ViewModel._isLoginService.SetTokkenAsync("true");

                }
            }
        }
        catch (Exception)
        {
            App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Đã có lỗi xảy ra🥲</text>
                            <image placement=""appLogoOverride"" hint-crop=""circle"" src=""https://i.ibb.co/94Ywqnm/Moon-Light-Logo.png""/>
                        </binding>
                    </visual>
                </toast>");
            //await App.MainWindow.ShowMessageDialogAsync("Đăng nhập thất bại", "Thất bại");
        }
        finally
        {
            if (ViewModel._isLoginService.IsLogin == "true")
            {
                var query = db.UserProfiles.Where(p => p.Id == id).FirstOrDefault();
                StaticDataBindingModel.AccountPicture = query.Avatar;
                StaticDataBindingModel.AccountName = query.Name;
                await ViewModel._isLoginService.SetTokkenAsync("true", _name: query.Name, _picture: query.Avatar);

                StaticDataBindingModel._isInput = false;
                ViewModel._navigationService.NavigateTo("Moon_Light_Music.ViewModels.TaiKhoanViewModel");
            }
        }
    }

    private async void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel._navigationService.NavigateTo("Moon_Light_Music.ViewModels.TaiKhoanLoginViewModel");
    }

    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        passworBoxWithRevealmode.Password = ViewModel._isLoginService.password;
    }

    private void tb_Email_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {

    }
}
