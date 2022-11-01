using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class DangTaiPage : Page
{
    public DangTaiViewModel ViewModel
    {
        get;
    }

    public DangTaiPage()
    {
        ViewModel = App.GetService<DangTaiViewModel>();
        InitializeComponent();
    }
}
