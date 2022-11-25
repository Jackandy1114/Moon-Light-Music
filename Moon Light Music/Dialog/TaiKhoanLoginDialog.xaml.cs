using Microsoft.UI.Xaml.Controls;

namespace Moon_Light_Music.Dialog
{
    public sealed partial class TaiKhoanLoginDialog : ContentDialog
    {
        private string _UserName;
        private string _Password;
        public string UserName
        {
            get => _UserName; set => _UserName = value;
        }

        public string Password
        {
            get => _Password; set => _Password = value;
        }
        public TaiKhoanLoginDialog()
        {
            this.InitializeComponent();
        }


    }
}
