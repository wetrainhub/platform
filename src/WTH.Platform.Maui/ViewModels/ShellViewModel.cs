using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using WTH.Platform.Maui.Localization;
using WTH.Platform.Maui.Messages;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Saas.Host;

namespace WTH.Platform.Maui.ViewModels;

public partial class ShellViewModel : PlatformViewModelBase,
    ITransientDependency
{
    [ObservableProperty]
    bool hasUsersPermission = true;

    [ObservableProperty]
    bool hasTenantsPermission = true;

    public bool IsIdentityUserPageVisible => CurrentUser.IsAuthenticated;

    public string CurrentUserName => CalculateUserFullName();

    public string ProfileImageUrl => RemoteServiceOptions.Value.RemoteServices.GetConfigurationOrDefaultOrNull("AbpAccountPublic")?.BaseUrl.TrimEnd('/')
        + $"/api/account/profile-picture-file/{CurrentUser.Id}?v=" + Guid.NewGuid();

    protected IOptions<AbpRemoteServiceOptions> RemoteServiceOptions { get; }

    public ShellViewModel(LocalizationResourceManager localizationManager, IOptions<AbpRemoteServiceOptions> remoteServiceOptions)
    {
        RemoteServiceOptions = remoteServiceOptions;

        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (r, m) =>
        {
            Task.Run(UpdatePermissions);
            UpdateProperties();
        });

        WeakReferenceMessenger.Default.Register<LogoutMessage>(this, (r, m) =>
        {
            Task.Run(UpdatePermissions);
            UpdateProperties();
        });

        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (r, m) =>
        {
            UpdateProperties();
        });

        WeakReferenceMessenger.Default.Register<WeakReferenceMessenger>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(ProfileImageUrl));
        });

        WeakReferenceMessenger.Default.Register<ProfilePictureChangedMessage>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(ProfileImageUrl));
        });

        localizationManager.PropertyChanged += (_, _) =>
        {
            UpdateProperties();
        };

        Task.Run(UpdatePermissions);
    }

    private void UpdateProperties()
    {
        OnPropertyChanged(nameof(IsIdentityUserPageVisible));
        OnPropertyChanged(nameof(CurrentUserName));
        OnPropertyChanged(nameof(ProfileImageUrl));
        OnPropertyChanged(nameof(CurrentUser));
    }

    [RelayCommand]
    private async Task UpdatePermissions()
    {
        HasUsersPermission = await AuthorizationService.IsGrantedAsync(IdentityPermissions.Users.Default);
        HasTenantsPermission = await AuthorizationService.IsGrantedAsync(SaasHostPermissions.Tenants.Default);
    }

    private string CalculateUserFullName()
    {
        var fullName = new StringBuilder();

        if (!CurrentUser.Name.IsNullOrEmpty())
        {
            fullName.Append(CurrentUser.Name);
        }

        if (!CurrentUser.SurName.IsNullOrEmpty())
        {
            if (fullName.Length > 0)
            {
                fullName.Append(' ');
            }

            fullName.Append(CurrentUser.SurName);
        }

        if (fullName.Length == 0)
        {
            fullName.Append(CurrentUser.UserName);
        }

        return fullName.ToString();
    }
}