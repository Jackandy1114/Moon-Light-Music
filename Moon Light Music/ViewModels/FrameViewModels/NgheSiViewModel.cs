using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;

public class NgheSiViewModel : ObservableRecipient
{

    public ObservableCollection<Artist> artists => StaticDataBindingModel.Artist;

    public List<Track> topTracks => StaticDataBindingModel.TopSpotifyTracks[0].Tracks;
    public NgheSiViewModel()
    {
    }
}
