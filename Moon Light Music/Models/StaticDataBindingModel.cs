using System.Collections.ObjectModel;

using Windows.Media.Playback;

namespace Moon_Light_Music.Models;
public static class StaticDataBindingModel
{
    public static string RequestSpotifyALbums
    {
        get; set;
    } = "";
    public static string RequestSpotifyTracks
    {
        get; set;
    } = "Love";

    public static string stringTo_RequestSpotifyTracks(string query = "Black Pink")
    {
        RequestSpotifyTracks = @$"https://api.spotify.com/v1/search?q={query}&type=track&market=VN&limit=20&offset=0";
        return RequestSpotifyTracks;
    }

    public static ObservableCollection<Item> AlbumsSpotify = new();
    public static ObservableCollection<Item> _TracksSpotify = new();
    public static ObservableCollection<Album> _AlbumSpotify = new();
    public static ObservableCollection<Item> TracksInAlbumsSpotify = new();
    public static ObservableCollection<Artist> Artist = new();

    public static Uri? _PLayingMedia
    {
        get; set;
    }
    public static MediaPlayer mediaPlayer
    {
        get; set;
    } = new();

    public static string song_img_url
    {
        get; set;
    } = @"\Image\Logo\1.png";

    public static string song_name { get; set; } = "Tên bài hát";

    public static bool _isEnableBtn_moreLoading = true;

}
