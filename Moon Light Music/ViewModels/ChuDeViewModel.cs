using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Dialog;
using Moon_Light_Music.Models;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Moon_Light_Music.ViewModels;

public class ChuDeViewModel : ObservableRecipient
{
    private readonly IOAuthTokkenService _oAuthTokkenService;
    public UIElement _shell;

    public ObservableCollection<Item> Songs
    {
        get; set;
    }

    private string _oAuthTokken;
    public string OAuthTokken
    {
        get => _oAuthTokken;
        set => SetProperty(ref _oAuthTokken, value);
    }

    public int _limit
    {
        get; set;
    } = 0;

    public ChuDeViewModel(IOAuthTokkenService oAuthTokkenService)
    {
        _limit += 10;
        _oAuthTokkenService = oAuthTokkenService;
        _oAuthTokken = _oAuthTokkenService.OAuthTokken;

        Songs = new ObservableCollection<Item>();
        PopulationCollection(_oAuthTokken);
    }


    async void PopulationCollection(string OAuth2Tokken)
    {
        var client = new RestClient();

        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{OAuth2Tokken}", "Bearer");
        var request = new RestRequest(@$"https://api.spotify.com/v1/browse/new-releases?country=VN&limit={_limit}&offset=10", Method.Get);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");
        var respone = new RestResponse();
        var data = new TrackModel();
        try
        {
            respone = client.GetAsync(request).GetAwaiter().GetResult();
            data = JsonConvert.DeserializeObject<TrackModel>(respone.Content);
            for (int i = 1; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                Songs.Add(track);
            }
        }
        catch (Exception)
        {

            ContentDialog _dialog = new ContentDialog()
            {
                XamlRoot = App.MainWindow.Content.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = "Nhập tokken mới",
                PrimaryButtonText = "Lưu",
                CloseButtonText = "Rời đi",
                DefaultButton = ContentDialogButton.Primary,
            };
            _dialog.Content = new TrangChuContentDiaglog();

            ContentDialogResult result = await _dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var _content = (TrangChuContentDiaglog)_dialog.Content;
                _oAuthTokken = _content.New_OAuthor2Tokken;

                await _oAuthTokkenService.SetTokkenAsync(_oAuthTokken);
            }
        }
        finally
        {
            //_shell = App.GetService<ChuDePage>();
            //App.MainWindow.Content = _shell ?? new Frame();
        }
    }
}
