using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class BaiHatPage : Page
{
    public BaiHatViewModel ViewModel
    {
        get;
    }

    public BaiHatPage()
    {
        ViewModel = App.GetService<BaiHatViewModel>();
        InitializeComponent();

    }
}
