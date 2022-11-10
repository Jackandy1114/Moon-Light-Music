using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Models;

namespace Moon_Light_Music.ViewModels;
public class ChuDeChildViewModel : ObservableRecipient
{
    public ObservableCollection<Item> TracksInAlbumsSpotify => StaticDataBindingModel.TracksInAlbumsSpotify;
    public ObservableCollection<Album> _AlbumSpotify => StaticDataBindingModel._AlbumSpotify;
    public ChuDeChildViewModel()
    {

    }
}
