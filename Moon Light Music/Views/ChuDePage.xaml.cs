using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Helpers;
using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class ChuDePage : Page
{

    public ChuDeViewModel ViewModel
    {
        get;
    }

    public NavigationHelper _navigationHelper;

    public ChuDePage()
    {
        ViewModel = App.GetService<ChuDeViewModel>();
        InitializeComponent();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //ViewModel._limit += 10;

    }
}
