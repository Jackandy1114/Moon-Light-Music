using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class PlayListPage : Page
{
    public PlayListViewModel ViewModel
    {
        get;
    }

    public PlayListPage()
    {
        ViewModel = App.GetService<PlayListViewModel>();
        InitializeComponent();
    }
}
