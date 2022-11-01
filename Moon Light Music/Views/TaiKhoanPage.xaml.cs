using Microsoft.UI.Xaml.Controls;

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
}
