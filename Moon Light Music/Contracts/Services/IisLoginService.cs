using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Light_Music.Contracts.Services;
public interface IisLoginService
{
    public string? IsLogin
    {
        get;
        set;
    }
     Task InitializeAsync();
    Task SetTokkenAsync(string tokken);
}
