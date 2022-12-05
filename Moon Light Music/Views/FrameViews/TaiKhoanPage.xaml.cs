using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class TaiKhoanPage : Page
{
    public TaiKhoanViewModel ViewModel
    {
        get;
    }
    public INavigationService _navigationService;
    public TaiKhoanPage()
    {
        ViewModel = App.GetService<TaiKhoanViewModel>();
        _navigationService = ViewModel._navigationService;
        InitializeComponent();
    }
    //Đang làm việc
    private async void HyperlinkButton_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        _navigationService.NavigateTo("Moon_Light_Music.ViewModels.TaiKhoanSignUpChildViewModel");


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


    private void PersonPicture_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {


    }
}
