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
        ContentDialog _LoginDialog = new TaiKhoanLoginDialog()
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
        };
        ContentDialogResult result = await _LoginDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var _content = (TaiKhoanLoginDialog)_LoginDialog.Content;
            var a = _content.UserName;
            var b = _content.Password;

        }

    }
}
