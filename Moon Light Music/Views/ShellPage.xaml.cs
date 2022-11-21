using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.ViewModels;

using Newtonsoft.Json;

using Windows.Media.Core;
using Windows.System;

namespace Moon_Light_Music.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class ShellPage : Page
{

    public ShellViewModel ViewModel
    {
        get;
    }

    public Microsoft.UI.Xaml.Controls.MediaPlayerElement _media => MainMPE;

    public INavigationService _navigationService;

    public string _song_img_url { get; set; } = StaticDataBindingModel.song_img_url;

    public string _song_name
    {
        get; set;
    } = StaticDataBindingModel.song_name;


    public string _logoTheme = "";
    public string LogoTheme
    {
        get => _logoTheme = ActualTheme == ElementTheme.Light ? "/Image/Logo/Light.png" : "/Image/Logo/Dark.png"; set => _logoTheme = value;
    }


    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        MainMPE.Source = MediaSource.CreateFromUri(new Uri("https://stream.nixcdn.com/NhacCuaTui1026/Psychofreak-CamilaCabelloWillowSmith-7182840.mp3?st=DEmuSFVapY4ThJvlRAKBew&e=1667985229"));

        _navigationService = ViewModel.NavigationService;

        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();

        ActualThemeChanged += (s, e) => Bindings.Update();

    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);

        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void LogoTheme_ActualThemeChanged(FrameworkElement sender, object args)
    {
        if (sender.ActualTheme == ElementTheme.Light)
        {
            LogoTheme = "/Image/Logo/Light.png";

        }
        else
        {
            LogoTheme = "/Image/Logo/Dark.png";

        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {

    }
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        //Rất xin lỗi nhạc của tui : )))) tại bạn để lộ cái player nên tui lấy thôi
        Uri manifestUri = new Uri("https://stream.nixcdn.com/NhacCuaTui1026/Psychofreak-CamilaCabelloWillowSmith-7182840.mp3?st=DEmuSFVapY4ThJvlRAKBew&e=1667985229");
        StaticDataBindingModel._PLayingMedia = manifestUri;
    }

    private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.QueryText))
        {
            StaticDataBindingModel._TracksSpotify = new();
            Tracks _tracks;
            var token = App._oAuthToken.Token;
            try
            {
                _tracks = JsonConvert.DeserializeObject<Tracks>(GetResponseFromAPIHelper.GetResponse(token, StaticDataBindingModel.stringTo_RequestSpotifyTracks(args.QueryText)).Content!)!;
                if (_tracks.Track != null)
                {
                    for (var i = 1; i < _tracks.Track.Limit; i++)
                    {
                        if (_tracks.Track.Items != null)
                        {
                            var track = _tracks.Track.Items[i];
                            StaticDataBindingModel._TracksSpotify.Add(track);
                        }
                    }
                    if (_tracks.Track.Next != null)
                    {
                        StaticDataBindingModel.RequestSpotifyTracks = _tracks.Track.Next.ToString()!;
                    }
                }
                _navigationService.NavigateTo("Moon_Light_Music.ViewModels.TrangChuViewModel");
                _navigationService.NavigateTo("Moon_Light_Music.ViewModels.BaiHatViewModel");
            }
            catch (Exception)
            {

            }
        }
    }
    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {

    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {

    }

    private void PersonPicture_Tapped(object sender, TappedRoutedEventArgs e)
    {
        //ViewModel.mediaSource = );
        //_media.MediaPlayer.SetUriSource(new Uri("https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309"));
    }

}
