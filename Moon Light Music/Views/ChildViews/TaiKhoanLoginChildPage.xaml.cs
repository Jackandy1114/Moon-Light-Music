﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using Moon_Light_Music.Contracts.Services;
using Moon_Light_Music.Helpers;
using Moon_Light_Music.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Moon_Light_Music.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TaiKhoanLoginChildPage : Page
{
    public bool _email_Check = false;
    public bool _password_Check = false;
    public bool _verifyPass_Check = false;
    public TaiKhoanLoginChildPage()
    {
        this.InitializeComponent();
    }


    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        var pass = sender as PasswordBox;
        if (string.IsNullOrEmpty(pass.Password))
        {
            Password_Check.Foreground = new SolidColorBrush(Colors.Yellow);
            _verifyPass_Check = false;
        }
        else
        {
            if (validationHelper.IsValidPassword(pass.Password))
            {
                Password_Check.Foreground = new SolidColorBrush(Colors.Green);
                _password_Check = true;
            }
            else
            {
                Password_Check.Foreground = new SolidColorBrush(Colors.Red);
                _password_Check = false;

            }
        }
    }


    private void email_TextChanged(object sender, TextChangedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            email_Check.Foreground = new SolidColorBrush(Colors.Yellow);
            _verifyPass_Check = false;
        }
        else
        {
            if (validationHelper.IsValidEmail(tb.Text))
            {
                email_Check.Foreground = new SolidColorBrush(Colors.Green);
                _email_Check = true;
            }
            else
            {
                email_Check.Foreground = new SolidColorBrush(Colors.Red);
                _email_Check = false;
            }

        }
    }

    private void verifyPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        var verifyPass = sender as PasswordBox;
        if (string.IsNullOrEmpty(verifyPass.Password))
        {
            verifyPass_Check.Foreground = new SolidColorBrush(Colors.Yellow);
            _verifyPass_Check = false;
        }
        else
        {
            if (verifyPass.Password == password.Password)
            {
                verifyPass_Check.Foreground = new SolidColorBrush(Colors.Green);
                _verifyPass_Check = true;
            }
            else
            {
                verifyPass_Check.Foreground = new SolidColorBrush(Colors.Red);
                _verifyPass_Check = false;

            }

        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (_email_Check && _password_Check && _verifyPass_Check)
        {
            using var db = new MoonLightMusicDataBaseContext();
            try
            {
                var query = db.UserAccounts.Where(p => p.Email == email.Text).FirstOrDefault();
                if (query != null)
                {
                    email_Check.Foreground = new SolidColorBrush(Colors.Red);
                    _email_Check = false;
                    App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Dùng email khác nha!🤨</text>
                        
                        </binding>
                    </visual>
                </toast>");
                }
                else
                {
                    var userIdProfile = "MoonLight" + Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                    var randomUserProfileName = "MoonLight " + Guid.NewGuid().ToString().Substring(0, 6);
                    var queryId = db.UserProfiles.Where(p => p.Id == userIdProfile).FirstOrDefault();
                    //Tạo ra một id mới nếu trùng
                    while (queryId != null)
                    {
                        userIdProfile = "MoonLight" + Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                    }
                    var userProfile = new UserProfile()
                    {
                        Id = userIdProfile,
                        Name = randomUserProfileName,
                        Avatar = @"ms-appx:///Image/Logo/1.png",
                    };
                    var user = new UserAccount()
                    {
                        Email = email.Text,
                        Password = password.Password,
                        Role = StaticDataBindingModel.roles[1],
                        UserProfile = userIdProfile
                    };

                    db.Add(userProfile);
                    await db.SaveChangesAsync();
                    db.Add(user);
                    await db.SaveChangesAsync();
                    App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Đăng ký thành công 😝</text>
                        </binding>
                    </visual>
                </toast>");


                }
            }
            catch (Exception)
            {
                App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Có gì đó sai sai🤨</text>
                            
                        </binding>
                    </visual>
                </toast>");
            }

        }
        else
        {
            App.GetService<IAppNotificationService>().Show(@"<toast>
                    <visual>
                        <binding template=""ToastGeneric"">
                            <text hint-maxLines=""1"">Trần Hoàng</text>
                            <text>❤️Hãy nhập đúng yêu cầu🤨</text>
                            
                        </binding>
                    </visual>
                </toast>");
        }

    }
}
