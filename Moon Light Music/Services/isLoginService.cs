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
    private const string SettingsKey = "IsLogin";

    private readonly ILocalSettingsService _localSettingsService;

    public isLoginService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task InitializeAsync()
    {
        IsLogin = await LoadTokkenFromSettingsAsync();
        await Task.CompletedTask;
    }
    private async Task<string> LoadTokkenFromSettingsAsync()
    {
        var tokken = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);
        return tokken != null ? tokken : "false";
    }
    private async Task SaveTokkenInSettingsAsync(string tokken)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, tokken);
    }

    public async Task SetTokkenAsync(string tokken)
    {
        IsLogin = tokken;
        await SaveTokkenInSettingsAsync(IsLogin);
    }
}
