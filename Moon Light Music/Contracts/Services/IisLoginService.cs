namespace Moon_Light_Music.Contracts.Services;
public interface IisLoginService
{
    public string? IsLogin
    {
        get;
        set;
    }
    public string? password
    {

        get; set;
    }
    public string? account
    {
        get; set;
    }
    public string? picture
    {
        get; set;
    }
    public string? name
    {
        get; set;
    }
    Task InitializeAsync();
    Task SetTokkenAsync(string tokken, string _password = null, string _account = null, string _name = null, string _picture = null);
}
