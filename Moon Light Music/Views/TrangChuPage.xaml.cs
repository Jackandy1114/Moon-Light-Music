using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class TrangChuPage : Page
{
    public TrangChuViewModel ViewModel
    {
        get;
    }

    public TrangChuPage()
    {
        ViewModel = App.GetService<TrangChuViewModel>();
        InitializeComponent();
    }
}
