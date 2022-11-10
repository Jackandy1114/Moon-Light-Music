using System.Collections.ObjectModel;

namespace Moon_Light_Music.Models;
public static class StaticDataBindingModel
{
    public static string RequestSpotifyALbums
    {
        get; set;
    } = "";
    public static ObservableCollection<Item> AlbumsSpotify = new();
    public static ObservableCollection<Album> _AlbumSpotify = new();
    public static ObservableCollection<Item> TracksInAlbumsSpotify = new();

    public static bool _isEnableBtn_moreLoading = true;

}
