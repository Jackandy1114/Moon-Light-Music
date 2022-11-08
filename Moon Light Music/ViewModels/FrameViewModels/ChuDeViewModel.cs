using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Dialog;
using Moon_Light_Music.Models;
using Moon_Light_Music.Views;

using Newtonsoft.Json;

using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Moon_Light_Music.ViewModels;

public class ChuDeViewModel : ObservableRecipient
{

    private readonly IOAuthTokkenService _oAuthTokkenService;

    private ObservableCollection<Item> _songs = new ObservableCollection<Item>()!;
    public ObservableCollection<Item> Songs
    {
        get => _songs; set => SetProperty(ref _songs, value);
    }

    private string _oAuthTokken;
    public string OAuthTokken
    {
        get => _oAuthTokken;
        set => SetProperty(ref _oAuthTokken, value);
    }

    private int _limit = 10;
    public int Limit
    {
        get => _limit;
        set => SetProperty(ref _limit, value);
    }
    private int _offset = 0;
    public int Offset
    {
        get => _offset;
        set => SetProperty(ref _offset, value);
    }

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }
    //public INavigationService _navigationService;

    public ICommand moreListcommand;

    public ChuDeViewModel(IOAuthTokkenService oAuthTokkenService, INavigationService navigationService, INavigationViewService navigationViewService)
    {
        NavigationService = navigationService;
        NavigationViewService = navigationViewService;

        _oAuthTokkenService = oAuthTokkenService;
        _oAuthTokken = _oAuthTokkenService.OAuthTokken;

        PopulationCollection(_oAuthTokken, Limit, Offset);
        moreListcommand = new RelayCommand(() =>
        {
            Limit += 10;
            Offset += 10;
            PopulationCollection(_oAuthTokken, Limit, Offset);
        });
    }

    async void PopulationCollection(string OAuth2Tokken, int Limit, int Offset)
    {
        var client = new RestClient();

        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{OAuth2Tokken}", "Bearer");
        //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{OAuth2Tokken}", "Basic");
        var request = new RestRequest(@$"https://api.spotify.com/v1/browse/new-releases?country=VN&limit={Limit}&offset={Offset}", Method.Get);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");
        RestResponse respone;
        TrackModel data;
        try
        {
            respone = client.GetAsync(request).GetAwaiter().GetResult();
            data = JsonConvert.DeserializeObject<TrackModel>(respone.Content!)!;
            if (data.Albums != null)
            {
                for (var i = 1; i < data.Albums.Limit; i++)
                {
                    if (data.Albums.Items != null)
                    {
                        var track = data.Albums.Items[i];
                        Songs.Add(track);
                    }
                }
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
                SecondaryButtonText = "Lấy tự động",

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
            else if (result == ContentDialogResult.Secondary)
            {

                getTokkenOnlineBySelenium();
                #region TestCommand
                //var _client = new RestClient();

                //var _t = Convert.ToBase64String(Encoding.UTF8.GetBytes("f636e05e4c5540d88ebd153ecc5a11a8" + ":" + "5d96de639d5b43248750ea080fffbc37"));
                //_client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{_t}", "Basic");
                //var _request = new RestRequest(@$"https://accounts.spotify.com/api/token", Method.Get);
                //_request.AddHeader("Accept", "application/json");
                //_request.AddHeader("Content-Type", "application/json");
                //RestResponse _respone;

                //_respone = client.GetAsync(request).GetAwaiter().GetResult();

                //await _oAuthTokkenService.SetTokkenAsync(_oAuthTokken);
                #endregion
            }

        }
        finally
        {
            //NavigationService.Frame.Navigate(typeof(TrangChuPage), "force refresh after language change");
        }


    }
    public async void getTokkenOnlineBySelenium()
    {
        //var service = OpenQA.Selenium.Edge.EdgeDriverService.CreateDefaultService(@"C:\Drivers", @"msedgedriver.exe");
        var service = EdgeDriverService.CreateDefaultService();
        //service.UseVerboseLogging = true;
        service.HideCommandPromptWindow = true;
        service.Start();
        EdgeOptions edgeOptions = new EdgeOptions();

        //edgeOptions.AddArguments(@"user-data-dir=C:\Users\PhuDatTran\AppData\Local\Microsoft\Edge\User Data");
        //edgeOptions.AddArguments("profile-directory=Profile 1");
        //edgeOptions.AddArguments("--start-maximized");

        //IWebDriver edge = new EdgeDriver(edgeOptions);

        //edgeOptions.PlatformName = "Windows 11";
        edgeOptions.BrowserVersion = "latest";

        var edge = new RemoteWebDriver(service.ServiceUrl, edgeOptions);
        var js = (IJavaScriptExecutor)edge;



        edge.Url = "https://developer.spotify.com/console/get-search-item/?q=&type=&market=&limit=&offset=&include_external=";
        edge.Navigate();
        //Thread.Sleep(2000);
        try
        {

            //edge.FindElement(By.CssSelector("#console-form > div.form-group.header-params > div > span > button")).Click();

            js.ExecuteScript(@"document.querySelector('#console-form > div.form-group.header-params > div > span > button').click()");

            //spotify
            edge.FindElement(By.XPath("//*[@id=\"oauth-modal\"]/div/div/div[2]/form")).Submit();

            //login way
            edge.FindElement(By.XPath(@"//*[@id=""root""]/div/div[2]/div/div/button[1]")).Click();

            //Facebook
            edge.FindElement(By.XPath("//*[@id=\"email\"]")).SendKeys(@"0985950723");
            edge.FindElement(By.XPath("//*[@id=\"pass\"]")).SendKeys(@"iloveuzienoi249");
            edge.FindElement(By.XPath(@"//*[@id=""loginbutton""]")).Click();

            var _inputTokken = (string)edge.FindElement(By.XPath(@"//*[@id=""oauth-input""]")).GetAttribute("value");

            //var _oatokken = (string)edge.FindElement(By.XPath("//*[@id=\"oauth-input\"]")).GetAttribute("value");
            if (_inputTokken != null)
            {
                _oAuthTokken = _inputTokken;
                await _oAuthTokkenService.SetTokkenAsync(_oAuthTokken);
            }
            else
            {
                await App.MainWindow.ShowMessageDialogAsync("Không thành công", "Có lỗi phát sinh");
            }

            //Thread.Sleep(2000);
        }
        catch (Exception)
        {
            await App.MainWindow.ShowMessageDialogAsync("Không thành công", "Có lỗi phát sinh");
        }
        finally
        {
            edge.Quit();
            if (NavigationService.Frame != null)
            {
                NavigationService.Frame.Navigate(typeof(ChuDePage), "force refresh after language change");
            }

            //service.Dispose();

        }
    }
}

