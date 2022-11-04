using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Dialog;
using Moon_Light_Music.Models;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Moon_Light_Music.ViewModels;

public class ChuDeViewModel : ObservableRecipient
{
    public ObservableCollection<Item> Songs
    {
        get; set;
    }

    public static string _oAuthTokken
    {
        get; set;
    }

    = @"BQA4poEwI8SuOy1E64deES9kZAGJnECDICbZLUVdKBc_OsTX5vJJYRvs8HyyG7ehdNtIfrPZH2oiksGAqL0iWjn72VZd5BRXNa2tVf535oTzqjWbf-Hs8Df_CFgGbFuG-2u1Z-5RbTZKp3EkgAX2BWaMB7zYZbjClC0SBKcjSCFacMubfQW7eXH306Y6TUsaLto"
    ;
    public ChuDeViewModel()
    {


        //OAuth2Tokken = (string)_dataContainer.Values["OAuthTokken"];

        //Task.Run(async () => OAuth2Tokken = await App.GetService<ILocalSettingsService>().ReadSettingAsync<string>("OAuthTokken"));

        //_oAuthTokken.Tokken = App.GetService<ILocalSettingsService>().ReadSettingAsync<string>("OAuthTokken").Result;
        Songs = new ObservableCollection<Item>();
        PopulationCollection(_oAuthTokken);
    }


    async void PopulationCollection(string OAuth2Tokken)
    {
        var client = new RestClient();

        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{OAuth2Tokken}", "Bearer");
        var request = new RestRequest(@"https://api.spotify.com/v1/browse/new-releases?country=VN&limit=30&offset=10", Method.Get);
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
                //_dataContainer.Values["OAuthTokken"] = OAuth2Tokken;
                //await App.GetService<ILocalSettingsService>().SaveSettingAsync("OAuthTokken", _oAuthTokken.Tokken);
            }
        }
        finally
        {
            App.MainWindow.Content.UpdateLayout();
        }
    }
}
