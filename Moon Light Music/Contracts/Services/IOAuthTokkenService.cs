namespace Moon_Light_Music.Contracts.Services;
public interface IOAuthTokkenService
{
    string OAuthTokken
    {
        get;
        set;
    }
    Task InitializeAsync();
    Task SetTokkenAsync(string tokken);

    Task SetRequestedTokkenAsync();

}
