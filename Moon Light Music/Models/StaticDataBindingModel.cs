﻿using System.Collections.ObjectModel;

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
    public static ObservableCollection<Item> TracksSpotify
    {
        get => tracksSpotify;
        set => tracksSpotify = value;
    }
    public static ObservableCollection<Item> AlbumsSpotify
    {
        get => albumsSpotify;
        set => albumsSpotify = value;
    }
    public static ObservableCollection<Album> AlbumSpotify
    {
        get => albumSpotify;
        set => albumSpotify = value;
    }
    public static ObservableCollection<Item> TracksInAlbumsSpotify
    {
        get => tracksInAlbumsSpotify;
        set => tracksInAlbumsSpotify = value;
    }
    public static ObservableCollection<Artist> Artist
    {
        get => artist;
        set => artist = value;
    }

    public static string StringToRequestSpotifyTracks(string query = "Black Pink")
    {
        RequestSpotifyTracks = @$"https://api.spotify.com/v1/search?q={query}&type=track&market=VN&limit=20&offset=0";
        return RequestSpotifyTracks;
    }

    private static ObservableCollection<Item> albumsSpotify = new();
    private static ObservableCollection<Item> tracksSpotify = new();
    private static ObservableCollection<Album> albumSpotify = new();
    private static ObservableCollection<Item> tracksInAlbumsSpotify = new();
    private static ObservableCollection<Artist> artist = new();


    //https://stream.nixcdn.com/NhacCuaTui940/MayonakaNoDoorStayWithMe-MikiMatsubara-4892669.mp3?st=PwGNSvVfkzi21atGdFwM4A&e=1669125309

    //https://stream.nixcdn.com/NhacCuaTui877/NuocNgoai-PhanManhQuynh-3640447.mp3?st=cGl4BcsPWLKV-km-pb08qg&e=1669176567


    public static bool _isEnableBtn_moreLoading = true;

}
