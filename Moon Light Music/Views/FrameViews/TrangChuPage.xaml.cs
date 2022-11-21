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

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //Rất xin lỗi nhạc của tui : )))) tại bạn để lộ cái player nên tui lấy thôi
        //Uri manifestUri = new Uri("https://stream.nixcdn.com/NhacCuaTui1026/Psychofreak-CamilaCabelloWillowSmith-7182840.mp3?st=DEmuSFVapY4ThJvlRAKBew&e=1667985229");
        //StaticDataBindingModel._PLayingMedia = manifestUri;
        //StaticDataBindingModel.mediaPlayer.Play();
    }
}
