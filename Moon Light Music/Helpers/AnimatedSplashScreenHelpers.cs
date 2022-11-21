using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Moon_Light_Music.Helpers;

class AnimatedSplashScreenHelpers
{
    /// <summary>
    /// Runs a given lottie animation full screen
    /// </summary>
    /// <param name="animationUri">eg: ms-appx:///Assets/splash.json</param>
    /// <returns></returns>
    public static async Task RunAnimatedSplashScreenAsync()
    {
        //The window needs to be active for the animation to play
        WindowEx SlashWindown = new WindowEx()
        {
            IsAlwaysOnTop = true,
            ExtendsContentIntoTitleBar = true,
            IsTitleBarVisible = false,
        };
        SlashWindown.Activate();
        //var originalWindowContent = App.MainWindow.Content;

        ////Create the element tree manually
        //var gridb = new Grid
        //{
        //    //Padding = new Thickness(40),
        //    Background = new SolidColorBrush(Colors.Transparent)
        //};

        //var tb = new TextBlock
        //{
        //    VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top,
        //    HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
        //    Text = "Moon Light Chào Bạn",

        //    FontSize = 54,
        //};



        //gridb.Children.Add(player);
        //gridb.Children.Add(tb);

        var player = new AnimatedVisualPlayer
        {
            Stretch = Stretch.Uniform,
            AutoPlay = false,
            RequestedTheme = Microsoft.UI.Xaml.ElementTheme.Default,
            Source = new AnimatedVisuals.Holloween(),

        };
        SlashWindown.Content = player;
        await player.PlayAsync(0, 1, false);
        SlashWindown.Close();

        //Thread.Sleep(2000);



        // Reset window content after the splashscreen animation has completed.

        //App.MainWindow.Content = originalWindowContent;
        App.MainWindow.Activate();

        await Task.CompletedTask;
    }


}
