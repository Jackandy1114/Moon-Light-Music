// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Dialog;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class OpenUrlMusicLink : ContentDialog
{
    private string? _link;
    public string Link
    {
        get => _link!; set => _link = value;
    }
    public OpenUrlMusicLink()
    {

        InitializeComponent();
    }
}
