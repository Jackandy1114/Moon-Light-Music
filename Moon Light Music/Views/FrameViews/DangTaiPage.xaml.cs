using System.Net;

using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

using Windows.Storage.Pickers;

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

    private async void Button_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        using (var wc = new WebClient())
        {
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            var st = "https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309";
            var uri = new Uri(st);
            var name = st.Split('/')[4].Split('?')[0];
            musicName.Text = name;
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

            //var a = Directory.GetCurrentDirectory();
            FolderPicker folderPicker = new FolderPicker()
            {
                CommitButtonText = "Mở",
            };
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            var folder = await folderPicker.PickSingleFolderAsync();

            wc.DownloadFileAsync(
                // Param1 = Link of file
                uri,
                // Param2 = Path to save
                @$"{folder.Path}/{name}"
            );
        }
    }
    void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        progressBar.Value = e.ProgressPercentage;
        if (progressBar.Value == 0)
        {
            DowloadStatus.Text = "Chưa tải";

        }
        if (progressBar.Value == 100)
        {
            DowloadStatus.Text = "Hoàn thành";
        }
        else
        {
            DowloadStatus.Text = "Đang tải";

        }
    }

}
