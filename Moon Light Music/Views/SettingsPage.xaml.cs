using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }


    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UIElement? _shell = App.GetService<ShellPage>();

        App.MainWindow.Content = _shell ?? new Frame();
    }

    //List<string> listIMG = new List<string>();

}
