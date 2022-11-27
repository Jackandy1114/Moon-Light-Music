using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Dialog;
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
    }

    private async void HyperlinkButton_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TaiKhoanLoginDialog _LoginDialog = new TaiKhoanLoginDialog()
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
        };
        ContentDialogResult result = await _LoginDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            //var a = (string)_LoginDialog.UserName;
            //var b = (string)_LoginDialog.Password;

        }

    }
}
