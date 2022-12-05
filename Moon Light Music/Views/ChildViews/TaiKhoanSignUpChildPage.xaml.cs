// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TaiKhoanSignUpChildPage : Page
{
    public INavigationService _navigationService;
    public TaiKhoanSignUpChildViewModel ViewModel
    {
        get;
    }
    public TaiKhoanSignUpChildPage()
    {
        ViewModel = App.GetService<TaiKhoanSignUpChildViewModel>();

        this.InitializeComponent();
    }
}
