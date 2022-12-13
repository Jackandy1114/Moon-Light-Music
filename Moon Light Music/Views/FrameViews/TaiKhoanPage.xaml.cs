using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Models;
using Moon_Light_Music.Services;
using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class TaiKhoanPage : Page
{
    public TaiKhoanViewModel ViewModel
    {
        get;
    }
    public TaiKhoanPage()
    {
        ViewModel = App.GetService<TaiKhoanViewModel>();
        
        InitializeComponent();
        if (ViewModel._isLoginService.IsLogin == "true")
        {
            tb_Name.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            tb_Email.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            btn_login.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            btn_logout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

        }
        else
        {
            tb_Name.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            tb_Email.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            ViewModel._isLoginService.picture = "ms-appx:///Image/Logo/1.png";
            ViewModel._isLoginService.name = "";
            btn_logout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            btn_login.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

        }
    }
    private async void Login_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        ViewModel._navigationService.NavigateTo("Moon_Light_Music.ViewModels.TaiKhoanSignUpChildViewModel");


        //TaiKhoanLoginDialog _LoginDialog = new TaiKhoanLoginDialog()
        //{
        //    XamlRoot = App.MainWindow.Content.XamlRoot,
        //    Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
        //};
        //ContentDialogResult result = await _LoginDialog.ShowAsync();
        //if (result == ContentDialogResult.Primary)
        //{


        //}
        //else if (result == ContentDialogResult.Secondary)
        //{
        //    SignUpDialog _SignupDialog = new SignUpDialog()
        //    {
        //        XamlRoot = App.MainWindow.Content.XamlRoot,
        //        Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
        //    };
        //    ContentDialogResult signUpResult = await _SignupDialog.ShowAsync();
        //}
    }
    private async void Logout_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel._isLoginService.SetTokkenAsync("false");
        ViewModel._navigationService.NavigateTo("Moon_Light_Music.ViewModels.TaiKhoanSignUpChildViewModel");

    }


    private void PersonPicture_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {


    }
}
