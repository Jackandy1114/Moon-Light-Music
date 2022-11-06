using System.Numerics;

using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Moon_Light_Music.Styles;
public partial class AlbumListView
{
    readonly Compositor _compositor = App.MainWindow.Compositor;
    SpringVector3NaturalMotionAnimation? _springAnimation;
    private void CreateOrUpdateSpringAnimation(float finalValue)
    {
        if (_springAnimation == null)
        {
            _springAnimation = _compositor.CreateSpringVector3Animation();
            _springAnimation.Target = "Scale";
        }

        _springAnimation.FinalValue = new Vector3(finalValue);
    }

    public void element_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        // Scale up to 1.5
        CreateOrUpdateSpringAnimation(1.5f);

        (sender as UIElement).StartAnimation(_springAnimation);

    }

    public void element_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        // Scale back down to 1.0
        CreateOrUpdateSpringAnimation(1.0f);

        (sender as UIElement).StartAnimation(_springAnimation);

    }
}
