﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Activation;
using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Views;

namespace Moon_Light_Music.Services;

public class ActivationService : IActivationService
{
    private readonly ActivationHandler<Microsoft.UI.Xaml.LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IOAuthTokkenService _oAuthTokkenService;
    private UIElement? _shell = null;

    public ActivationService(ActivationHandler<Microsoft.UI.Xaml.LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService, IOAuthTokkenService oAuthTokkenService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
        _oAuthTokkenService = oAuthTokkenService;
    }

    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await InitializeAsync();

        if (App.MainWindow.Content == null)
        {
            _shell = App.GetService<ShellPage>();
            App.MainWindow.Content = _shell ?? new Frame();
        }

        // Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        App.MainWindow.Activate();

        // Execute tasks after activation.
        await StartupAsync();
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    private async Task InitializeAsync()
    {
        await _themeSelectorService.InitializeAsync().ConfigureAwait(false);
        await _oAuthTokkenService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        await _themeSelectorService.SetRequestedThemeAsync();
        //await _oAuthTokkenService.SetRequestedTokkenAsync();
        await Task.CompletedTask;
    }
    //void DismissExtendedSplash()
    //{
    //    //Navigate to mainpage

    //}
}
