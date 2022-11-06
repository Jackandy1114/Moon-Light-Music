using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Dialog;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TrangChuContentDiaglog : Page
{
    private string? _new_OAuthor2Tokken;
    public string New_OAuthor2Tokken
    {
        get => _new_OAuthor2Tokken!; set => _new_OAuthor2Tokken = value;
    }
    public TrangChuContentDiaglog()
    {
        this.InitializeComponent();
    }
}
