using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.Services;
public class isLoginService : IisLoginService
{
    public string? IsLogin
    {
        get; set;
    }
    public string? password
    {

        get;set;
    }
    public string? account
    {

        get; set;
    }
    public string? name
    {

        get; set;
    }
    public string? picture
    {

        get; set;
    }
    private const string SettingsKey = "IsLogin";

    private readonly ILocalSettingsService _localSettingsService;

    public isLoginService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task InitializeAsync()
    {
        IsLogin = await LoadTokkenFromSettingsAsync();
        password= await LoadFromSettingsAsync("Password");
        account = await LoadFromSettingsAsync("Account");
        name= await LoadFromSettingsAsync("Name");
        picture = await LoadFromSettingsAsync("Picture");
        await Task.CompletedTask;
    }
    private async Task<string> LoadTokkenFromSettingsAsync()
    {
        var tokken = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);
        return tokken != null ? tokken : "false";
    }
    private async Task<string> LoadFromSettingsAsync(string setting=SettingsKey)
    {
        var tokken = await _localSettingsService.ReadSettingAsync<string>(setting);
        if (setting == "Picture")
        {
            return tokken != null ? tokken : "ms-appx:///Image/Logo/1.png";
        }
        return tokken != null ? tokken : "";
    }
    
    private async Task SaveTokkenInSettingsAsync(string tokken)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, tokken);
    }

    public async Task SetTokkenAsync(string tokken,string _password=null,string _account=null,string _name = null,string _picture = null)
    {
        IsLogin = tokken;
        await SaveTokkenInSettingsAsync(IsLogin);
        if (_password!=null)
        {
        password =_password;
        await _localSettingsService.SaveSettingAsync("Password", password);

        }
        if (_account!=null)
        {
        account = _account;
        await _localSettingsService.SaveSettingAsync("Account", account);


        }
        if (_name!=null)
        {

            name = _name;
            await _localSettingsService.SaveSettingAsync("Name", name);
        }
        if (_picture!=null)
        {
            picture = _picture;
            await _localSettingsService.SaveSettingAsync("Picture", picture);

        }
    }
}
