using System.Net;

using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using Moon_Light_Music.ViewModels;

using Windows.Storage.Pickers;

namespace Moon_Light_Music.Views;

public sealed partial class DangTaiPage : Page
{
    public DangTaiViewModel ViewModel
    {
        get;
    }
    public List<string> ListLinkDownload;
    public DangTaiPage()
    {
        ViewModel = App.GetService<DangTaiViewModel>();
        InitializeComponent();
    }
    //https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309
    private async void Button_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ListLinkDownload = new List<string>();
        for (int i = ListTb_LinkTai.Items.Count - 1; i > 0; i--)
        {
            var item = ListTb_LinkTai.Items[i] as TextBox;
            if (string.IsNullOrEmpty(item.Text))
            {
                ListTb_LinkTai.Items.RemoveAt(i);
            }
            else
            {
                ListLinkDownload.Add(item.Text);
            }
        }

        if (ListTb_LinkTai.Items.Count == 0 || ListLinkDownload.Count == 0)
        {

        }
        else
        {

            using (var wc = new WebClient())
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                //var a = Directory.GetCurrentDirectory();
                FolderPicker folderPicker = new FolderPicker()
                {
                    CommitButtonText = "Mở",

                };
                WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
                var folder = await folderPicker.PickSingleFolderAsync();
                if (folder != null)
                {

                    Lb_DaTai.Items.Clear();
                    foreach (var item in ListLinkDownload)
                    {
                        string? name = null;
                        try
                        {
                            name = item.Split('/')[4].Split('?')[0];
                            musicName.Text = name;
                            var uri = new Uri(item);

                            await wc.DownloadFileTaskAsync(uri, @$"{folder.Path}/{name}");
                            //Xử lý multiple dowload file
                            Lb_DaTai.Items.Add(new TextBlock() { Tag = item, Text = name == null ? "Tên hỏng nhưng đã tải về" : name, Foreground = new SolidColorBrush(Colors.Green) });

                        }
                        catch (Exception)
                        {
                            Lb_DaTai.Items.Add(new TextBlock() { Text = name == null ? "Lỗi" : name, Foreground = new SolidColorBrush(Colors.Red) });

                        }
                    }


                }

            }
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

    private void ThemTb_LinkTai_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ListTb_LinkTai.Items.Add(new TextBox() { PlaceholderText = "Nhập link tải nhạc" });
    }
    private void XoaTb_LinkTai_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {


        if (ListTb_LinkTai.Items.Count != 0)
        {
            ListTb_LinkTai.Items.RemoveAt(ListTb_LinkTai.Items.Count - 1);
        }
    }
}
