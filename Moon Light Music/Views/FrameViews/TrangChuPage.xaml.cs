﻿using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.ViewModels;

using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Moon_Light_Music.Views;

public sealed partial class TrangChuPage : Page
{
    public ShellPage _shell
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
        _shell = (ShellPage)App.MainWindow.Content;
        InitializeComponent();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //Rất xin lỗi nhạc của tui : )))) tại bạn để lộ cái player nên tui lấy thôi
        //Uri manifestUri = new Uri("https://stream.nixcdn.com/NhacCuaTui1026/Psychofreak-CamilaCabelloWillowSmith-7182840.mp3?st=DEmuSFVapY4ThJvlRAKBew&e=1667985229");
        //StaticDataBindingModel._PLayingMedia = manifestUri;
        //StaticDataBindingModel.mediaPlayer.Play();
    }

    private async void MenuFlyoutItem_TappedAsync(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

        FileOpenPicker openPicker = new FileOpenPicker();
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        var fileTypes = new string[] { ".wmv", ".mp4", ".mkv", ".mp3" };

        //Add your fileTypes to the FileTypeFilter list of filePicker.
        foreach (var fileType in fileTypes)
        {
            openPicker.FileTypeFilter.Add(fileType);
        }
        openPicker.FileTypeFilter.Add("*");
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);


        StorageFile file = await openPicker.PickSingleFileAsync();
        if (file != null)
        {
            if (_shell._media.MediaPlayer.Source != MediaSource.CreateFromStorageFile(file))
            {
                _shell._media.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                _shell._media.MediaPlayer.Play();
            }

        }
    }

    private void MenuFlyoutItem_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        if (_shell._media.MediaPlayer.Source != MediaSource.CreateFromUri(new Uri("https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309")))
        {

            _shell._media.MediaPlayer.SetUriSource(new Uri("https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309"));
            _shell._media.MediaPlayer.Play();

        }
    }
}
