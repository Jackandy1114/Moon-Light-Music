using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using Moon_Light_Music.Activation;
using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Core.Contracts.Services;
using Moon_Light_Music.Core.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;
using Moon_Light_Music.Notifications;
using Moon_Light_Music.Services;
using Moon_Light_Music.ViewModels;
using Moon_Light_Music.Views;

using Newtonsoft.Json;

namespace Moon_Light_Music;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging

    public static GetAPI? _oAuthToken
    {
        get
        {
            try
            {
                return JsonConvert.DeserializeObject<GetAPI>(GetResponseFromAPIHelper.GetResponse_AndToken().Content!)!;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow
    {
        get;
    } = new MainWindow()
    {
        ExtendsContentIntoTitleBar = true,

    };

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IOAuthTokkenService, OAuthTokkenService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IFileService, FileService>();

            // Views and ViewModels

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<DanhGiaViewModel>();
            services.AddTransient<DanhGiaPage>();
            services.AddTransient<TaiKhoanViewModel>();
            services.AddTransient<TaiKhoanPage>();
            services.AddTransient<DangTaiViewModel>();
            services.AddTransient<DangTaiPage>();
            services.AddTransient<VideoViewModel>();
            services.AddTransient<VideoPage>();
            services.AddTransient<PlayListViewModel>();
            services.AddTransient<PlayListPage>();
            services.AddTransient<XepHangViewModel>();
            services.AddTransient<XepHangPage>();
            services.AddTransient<NgheSiViewModel>();
            services.AddTransient<NgheSiPage>();
            services.AddTransient<BaiHatViewModel>();
            services.AddTransient<BaiHatPage>();
            services.AddTransient<ChuDeViewModel>();
            services.AddTransient<ChuDePage>();
            services.AddTransient<ChuDeChildPage>();
            services.AddTransient<ChuDeChildViewModel>();
            services.AddTransient<TrangChuViewModel>();
            services.AddTransient<TrangChuPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<TaiKhoanSignUpChildPage>();
            services.AddTransient<TaiKhoanSignUpChildViewModel>();
            services.AddTransient<TaiKhoanLoginChildPage>();
            services.AddTransient<TaiKhoanLoginViewModel>();


            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            //services.Configure<OAuthTokken>(context.Configuration.GetSection(nameof(OAuthTokken)));
        }).
        Build();
        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }
    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        //CultureInfo.CurrentCulture = new CultureInfo("en-US");
        //CultureInfo.CurrentUICulture = new CultureInfo("en-US");

        base.OnLaunched(args);

        //Code này hiện thông báo lúc mở chương trình un comment khi release

        App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);



    }
}
