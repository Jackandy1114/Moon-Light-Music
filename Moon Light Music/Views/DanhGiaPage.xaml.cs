using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

namespace Moon_Light_Music.Views;

public sealed partial class DanhGiaPage : Page
{
    public DanhGiaViewModel ViewModel
    {
        get;
    }

    public DanhGiaPage()
    {
        ViewModel = App.GetService<DanhGiaViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {


    }
}
