using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class XepHangPage : Page
{
    public XepHangViewModel ViewModel
    {
        get;
    }

    public XepHangPage()
    {
        ViewModel = App.GetService<XepHangViewModel>();
        InitializeComponent();
    }
}
