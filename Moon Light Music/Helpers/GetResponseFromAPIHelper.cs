using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Moon_Light_Music.Helpers;
public static class GetResponseFromAPIHelper
{
    public static RestResponse GetResponse_AndToken()
    {
        var clientid = "f636e05e4c5540d88ebd153ecc5a11a8";
        var clientsecret = "5d96de639d5b43248750ea080fffbc37";
        var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientid, clientsecret)));

        var client = new RestClient();
        //Spotify
        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{encode_clientid_clientsecret}", "Basic");
        var request = new RestRequest(@$"https://accounts.spotify.com/api/token?grant_type=client_credentials&client_id={clientid}&client_secret={clientsecret}", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        return client.PostAsync(request).GetAwaiter().GetResult();
    }
    public static RestResponse GetResponse(string OAuth2Tokken, string _request)
    {
        var client = new RestClient();

        //Spotify
        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"{OAuth2Tokken}", "Bearer");
        var request = new RestRequest(_request, Method.Get);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        return client.GetAsync(request).GetAwaiter().GetResult();
    }
    public static async Task<string> getTokkenOnlineBySelenium()
    {
        var service = EdgeDriverService.CreateDefaultService();
        service.HideCommandPromptWindow = true;
        service.Start();

        EdgeOptions edgeOptions = new EdgeOptions();
        edgeOptions.BrowserVersion = "latest";

        var edge = new RemoteWebDriver(service.ServiceUrl, edgeOptions);
        var js = (IJavaScriptExecutor)edge;

        edge.Url = "https://developer.spotify.com/console/get-search-item/?q=&type=&market=&limit=&offset=&include_external=";
        edge.Navigate();
        var tokken = "";
        try
        {
            js.ExecuteScript(@"document.querySelector('#console-form > div.form-group.header-params > div > span > button').click()");

            //spotify
            edge.FindElement(By.XPath("//*[@id=\"oauth-modal\"]/div/div/div[2]/form")).Submit();

            //login way
            edge.FindElement(By.XPath(@"//*[@id=""root""]/div/div[2]/div/div/button[1]")).Click();

            //Facebook
            edge.FindElement(By.XPath("//*[@id=\"email\"]")).SendKeys(@"0985950723");
            edge.FindElement(By.XPath("//*[@id=\"pass\"]")).SendKeys(@"iloveuzienoi249");
            edge.FindElement(By.XPath(@"//*[@id=""loginbutton""]")).Click();
            tokken = (string)edge.FindElement(By.XPath(@"//*[@id=""oauth-input""]")).GetAttribute("value");
        }
        catch (Exception)
        {
            await App.MainWindow.ShowMessageDialogAsync("Không thành công", "Có lỗi phát sinh");
            tokken = "fail";
        }
        finally
        {
            edge.Quit();
        }
        return tokken;
    }
}
