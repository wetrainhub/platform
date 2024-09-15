using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Options;
using WTH.Platform.Maui.Messages;
using WTH.Platform.Maui.Pages;
using WTH.Platform.Maui.Settings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace WTH.Platform.Maui.ViewModels;

public partial class SettingsViewModel : PlatformViewModelBase, ITransientDependency
{
    protected ThemeManager ThemeManager { get; }

    protected AbpLocalizationOptions LocalizationOptions { get; }

    public string? CurrentLanguage => L.CurrentCulture!.DisplayName;

    public string? CurrentTheme => L[App.Current!.UserAppTheme.ToString()];

    public SettingsViewModel(
        ThemeManager themeManager,
        IOptions<AbpLocalizationOptions> localizationOptions)
    {
        ThemeManager = themeManager;
        LocalizationOptions = localizationOptions.Value;

        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(CurrentTenant));
        });

        WeakReferenceMessenger.Default.Register<LogoutMessage>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(CurrentTenant));
        });
    }

    [RelayCommand]
    async Task ChangeLanguage()
    {
        var selectedLanguage = await Application.Current!.MainPage!
            .DisplayActionSheet(L["Language"], L["Cancel"], null,
            LocalizationOptions.Languages.Select(x => x.DisplayName).ToArray());

        if (selectedLanguage.IsNullOrWhiteSpace())
        {
            return;
        }

        var culture = LocalizationOptions.Languages.FirstOrDefault(x => x.DisplayName == selectedLanguage);
        if (culture == null)
        {
            return;
        }
        var cultureInfo = new CultureInfo(culture.CultureName);

        L.CurrentCulture = cultureInfo;

        Shell.Current.FlowDirection = cultureInfo.TextInfo.IsRightToLeft
            ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        InvokePropertyChanged();
    }

    [RelayCommand]
    async Task ChangeTheme()
    {
        var themeOptions = new Dictionary<string, AppTheme>()
        {
            { L["Unspecified"], AppTheme.Unspecified },
            { L["Light"],AppTheme.Light },
            { L["Dark"] ,AppTheme.Dark },
        };

        var currentTheme = await ThemeManager.GetAppThemeAsync();

        var selectedTheme = await Application.Current!.MainPage!
            .DisplayActionSheet(L["DeviceTheme"], L["Cancel"], null,
            themeOptions.Select(x => x.Key).ToArray());

        if (selectedTheme.IsNullOrEmpty())
        {
            return;
        }

        await ThemeManager.SetAppThemeAsync(themeOptions[selectedTheme]);
        InvokePropertyChanged();
    }

    [RelayCommand]
    async Task NavigateToPicture()
    {
        await Shell.Current.GoToAsync(nameof(ProfilePicturePage));
    }

    [RelayCommand]
    async Task NavigateToChangePassword()
    {
        await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
    }

    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync(nameof(LoginOrLogoutPage));
    }

    protected void InvokePropertyChanged()
    {
        OnPropertyChanged(nameof(CurrentTheme));
        OnPropertyChanged(nameof(CurrentLanguage));
    }
}
