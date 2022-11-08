using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class NgheSiPage : Page
{
    public NgheSiViewModel ViewModel
    {
        get;
    }

    public NgheSiPage()
    {
        ViewModel = App.GetService<NgheSiViewModel>();
        InitializeComponent();
    }
}
