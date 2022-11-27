using Microsoft.UI.Xaml.Controls;

namespace Moon_Light_Music.Dialog;

public sealed partial class TaiKhoanLoginDialog : ContentDialog
{
    public string? UserName
    {
        get; set;
    }

    public string? Password
    {
        get; set;
    }
    public TaiKhoanLoginDialog()
    {
        InitializeComponent();
    }
}
