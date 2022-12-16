using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.Services;
public class OAuthTokkenService : IOAuthTokkenService
{
    public string? OAuthTokken
    {
        get; set;
    }
    private const string SettingsKey = "OAuthTokken";

    private readonly ILocalSettingsService _localSettingsService;

    public OAuthTokkenService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task InitializeAsync()
    {
        OAuthTokken = await LoadTokkenFromSettingsAsync();
        await Task.CompletedTask;
    }
    private async Task<string> LoadTokkenFromSettingsAsync()
    {
        var tokken = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);

        return tokken != null ? tokken : "null";
    }
    private async Task SaveTokkenInSettingsAsync(string tokken)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, tokken);
    }

    public async Task SetTokkenAsync(string tokken)
    {
        OAuthTokken = tokken;
        await SaveTokkenInSettingsAsync(OAuthTokken);
    }

}
