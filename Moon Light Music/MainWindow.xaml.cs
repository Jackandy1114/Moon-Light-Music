using Moon_Light_Music.Helpers;

namespace Moon_Light_Music;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }

}

// Server=   ;Initial Catalog=MoonLightMusicDataBase;Persist Security Info=False;User ID={your_username};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication="Active Directory Integrated";

// Scaffold-DbContext "Server=tcp:moonlightmusic.database.windows.net,1433; User ID=jayandy;Password=Iloveuzienoi1114 ;Database=MoonLightMusicDataBase;Trusted_Connection=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f