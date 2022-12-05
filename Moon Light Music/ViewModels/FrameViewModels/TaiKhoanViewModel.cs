﻿using CommunityToolkit.Mvvm.ComponentModel;

using Moon_Light_Music.Contracts.Services;

namespace Moon_Light_Music.ViewModels;

public class TaiKhoanViewModel : ObservableRecipient
{
    public INavigationService _navigationService;
    public TaiKhoanViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
