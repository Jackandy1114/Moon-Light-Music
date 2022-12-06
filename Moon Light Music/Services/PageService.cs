using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.ViewModels;
using Moon_Light_Music.Views;

namespace Moon_Light_Music.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<TrangChuViewModel, TrangChuPage>();
        Configure<ChuDeViewModel, ChuDePage>();
        Configure<BaiHatViewModel, BaiHatPage>();
        Configure<NgheSiViewModel, NgheSiPage>();
        Configure<XepHangViewModel, XepHangPage>();
        Configure<PlayListViewModel, PlayListPage>();
        Configure<VideoViewModel, VideoPage>();
        Configure<DangTaiViewModel, DangTaiPage>();
        Configure<TaiKhoanViewModel, TaiKhoanPage>();
        Configure<DanhGiaViewModel, DanhGiaPage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<ChuDeChildViewModel, ChuDeChildPage>();
        Configure<TaiKhoanSignUpChildViewModel, TaiKhoanSignUpChildPage>();
        Configure<TaiKhoanLoginViewModel, TaiKhoanLoginChildPage>();
        Configure<ShellViewModel, ShellPage>();

    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
