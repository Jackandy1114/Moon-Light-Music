using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

using Moon_Light_Music.Dialog;
using Moon_Light_Music.ViewModels;

using Windows.Media.Core;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;

namespace Moon_Light_Music.Views;

public sealed partial class TrangChuPage : Page
{
    public ShellPage Shell
    {
        get;
    }
    public TrangChuViewModel ViewModel
    {
        get;
    }

    public TrangChuPage()
    {
        ViewModel = App.GetService<TrangChuViewModel>();
        Shell = (ShellPage)App.MainWindow.Content;
        InitializeComponent();
    }

    private async void MenuFlyoutOpenFile_TappedAsync(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

        var openPicker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.PicturesLibrary
        };
        var fileTypes = new string[] { ".wmv", ".mp4", ".mkv", ".mp3" };

        //Add your fileTypes to the FileTypeFilter list of filePicker.
        foreach (var fileType in fileTypes)
        {
            openPicker.FileTypeFilter.Add(fileType);
        }
        openPicker.FileTypeFilter.Add("*");
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);


        var file = await openPicker.PickSingleFileAsync();
        if (file != null)
        {
            using (var thumbnail = await file.GetScaledImageAsThumbnailAsync(ThumbnailMode.SingleItem))
            {
                if (thumbnail != null)
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(thumbnail);

                    Shell._MediaPicture.ProfilePicture = bitmapImage;
                }
                else
                {

                }
            }
            //Cần if

            Shell.Media.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
            Shell.Media.MediaPlayer.Play();


        }
    }

    private async void MenuFlyoutOpenLink_TappedAsync(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        var _dialog = new OpenUrlMusicLink()
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,

        };

        ContentDialogResult result = await _dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {

            //Cần if

            //if (_shell._media.MediaPlayer.Source != MediaSource.CreateFromUri(new Uri(_dialog.Link)))
            //{
            Shell.Media.MediaPlayer.SetUriSource(new Uri(_dialog.Link));

            Shell.Media.MediaPlayer.Play();
            //}
        }

    }
}
