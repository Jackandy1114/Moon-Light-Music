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
    public BaiHatPage BaiHatPage
    {
        get;
    }

    public Microsoft.UI.Xaml.Controls.MediaPlayerElement Media => MainMPE;
    public PersonPicture _MediaPicture => Shell_MediaPicture;

    public TextBlock _MediaName => Shell_MediaName;
    public TextBlock MediaArtist => Shell_MediaArtist;
    public string _logoTheme = "";
    public string LogoTheme
    {
        get => _logoTheme = ActualTheme == ElementTheme.Light ? @"ms-appx:///Image/Logo/Light.png" : @"ms-appx:///Image/Logo/Dark.png"; set => _logoTheme = value;
    }


    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        BaiHatPage = App.GetService<BaiHatPage>();
        MainMPE.Source = MediaSource.CreateFromUri(new Uri("https://stream.nixcdn.com/NhacCuaTui1026/Psychofreak-CamilaCabelloWillowSmith-7182840.mp3?st=DEmuSFVapY4ThJvlRAKBew&e=1667985229"));


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

    private void MainWindow_Activated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs args)
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
            LogoTheme = @"ms-appx:///Image/Logo/Light.png";

        }
        else
        {
            LogoTheme = @"ms-appx:///Image/Logo/Dark.png";

        }
    }

    private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.QueryText))
        {
            StaticDataBindingModel.TracksSpotify = new();
            Tracks? _tracks = null;
            try
            {
                _tracks = JsonConvert.DeserializeObject<Tracks>(GetResponseFromAPIHelper.GetResponse(ViewModel.OAuthTokkenService.OAuthTokken, StaticDataBindingModel.StringToRequestSpotifyTracks(args.QueryText)).Content!);
            }
            catch (Exception)
            {

            }
            if (_tracks == null)
            {
                BaiHatPage.Btn_moreList.IsEnabled = false;
            }
            else if (_tracks.Track == null)
            {
                BaiHatPage.Btn_moreList.IsEnabled = false;
            }
            else
            {
                if (_tracks.Track.Items != null)
                {
                    for (var i = 0; i < _tracks.Track.Items.Count; i++)
                    {
                        if (_tracks.Track.Items != null)
                        {
                            var track = _tracks.Track.Items[i];
                            StaticDataBindingModel.TracksSpotify.Add(track);
                        }
                    }
                    if (_tracks.Track.Next != null)
                    {
                        StaticDataBindingModel.RequestSpotifyTracks = _tracks.Track.Next.ToString()!;
                    }
                    else
                    {
                        BaiHatPage.Btn_moreList.IsEnabled = false;
                    }
                }
            }
            ViewModel.NavigationService.NavigateTo("Moon_Light_Music.ViewModels.XepHangViewModel");
            ViewModel.NavigationService.NavigateTo("Moon_Light_Music.ViewModels.BaiHatViewModel");
        }
    }
    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {

    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {

    }

    private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var btn = (AppBarToggleButton)sender;
        if (btn.IsChecked == true)
        {
            Media.MediaPlayer.IsLoopingEnabled = true;

        }
        else
        {
            Media.MediaPlayer.IsLoopingEnabled = false;

        }
    }

    private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e != null && e.Key == VirtualKey.Space)
        {
            if (Media.MediaPlayer.CurrentState == Windows.Media.Playback.MediaPlayerState.Playing)
            {
                Media.MediaPlayer.Pause();

            }
            else
            {
                Media.MediaPlayer.Play();

            }
        }
    }
}
