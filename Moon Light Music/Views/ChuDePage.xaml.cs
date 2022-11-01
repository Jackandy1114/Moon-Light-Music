using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class ChuDePage : Page
{
    public ChuDeViewModel ViewModel
    {
        get;
    }

    public ChuDePage()
    {
        ViewModel = App.GetService<ChuDeViewModel>();
        InitializeComponent();
    }
}
