using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Options;
using WTH.Platform.Maui.Messages;
using WTH.Platform.Maui.Pages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Abp.Threading;

namespace WTH.Platform.Maui.ViewModels;
public partial class IdentityUserPageViewModel : PlatformViewModelBase,
    IRecipient<IdentityUserCreateMessage>, //create
    IRecipient<IdentityUserEditMessage>, //edit
    ITransientDependency
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    bool isLoadingMore;

    [ObservableProperty]
    bool canLoadMore;

    public GetIdentityUsersInput Input { get; } = new();

    public ObservableCollection<IdentityUserDto> Items { get; } = new();

    protected IIdentityUserAppService IdentityUserAppService { get; }

    protected IOptions<AbpRemoteServiceOptions> RemoteServiceOptions { get; }

    protected SemaphoreSlim SemaphoreSlim { get; } = new SemaphoreSlim(1, 1);

    public string? AbpAccountPublicUrl => RemoteServiceOptions.Value.RemoteServices
        .GetConfigurationOrDefaultOrNull("AbpAccountPublic")?.BaseUrl.TrimEnd('/');

    public IdentityUserPageViewModel(
        IIdentityUserAppService identityUserAppService,
        IOptions<AbpRemoteServiceOptions> remoteServiceOptions)
    {
        IdentityUserAppService = identityUserAppService;
        RemoteServiceOptions = remoteServiceOptions;

        WeakReferenceMessenger.Default.Register<IdentityUserCreateMessage>(this);
        WeakReferenceMessenger.Default.Register<IdentityUserEditMessage>(this);
    }

    [RelayCommand]
    async Task OpenCreateModal()
    {
        await Shell.Current.GoToAsync(nameof(IdentityUserCreateModalPage));
    }

    [RelayCommand]
    async Task OpenEditModal(Guid userId)
    {
        await Shell.Current.GoToAsync($"{nameof(IdentityUserEditModalPage)}?UserId={userId}");
    }

    [RelayCommand]
    async Task Refresh()
    {
        await GetUsersAsync();
    }

    [RelayCommand]
    async Task ShowActions(IdentityUserDto user)
    {
        var result = await App.Current!.MainPage!.DisplayActionSheet(
            L["Actions"],
            L["Cancel"],
            null,
            L["Edit"], L["Delete"]);

        if (result == L["Edit"])
        {
            await OpenEditModal(user.Id);
        }

        if (result == L["Delete"])
        {
            await Delete(user);
        }
    }

    [RelayCommand]
    async Task Delete(IdentityUserDto user)
    {
        var confirmed = await Shell.Current.CurrentPage.DisplayAlert(
            L["Delete"],
            string.Format(L["UserDeletionConfirmationMessage"].Value, user.UserName),
            L["Delete"],
            L["Cancel"]);

        if (!confirmed)
        {
            return;
        }

        try
        {
            await IdentityUserAppService.DeleteAsync(user.Id);
        }
        catch (AbpRemoteCallException remoteException)
        {
            HandleException(remoteException);
        }

        await GetUsersAsync();
    }

    private async Task GetUsersAsync()
    {
        IsBusy = true;

        try
        {
            Input.SkipCount = 0;
            var result = await IdentityUserAppService.GetListAsync(Input);

            Items.Clear();
            foreach (var user in result.Items)
            {
                Items.Add(user);
            }

            CanLoadMore = result.Items.Count >= Input.MaxResultCount;

        }
        catch (AbpRemoteCallException remoteException)
        {
            HandleException(remoteException);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task LoadMore()
    {
        if (!CanLoadMore)
        {
            return;
        }

        try
        {
            using (await SemaphoreSlim.LockAsync())
            {
                IsLoadingMore = true;

                Input.SkipCount += Input.MaxResultCount;

                var result = await IdentityUserAppService.GetListAsync(Input);

                CanLoadMore = result.Items.Count >= Input.MaxResultCount;

                foreach (var tenant in result.Items)
                {
                    Items.Add(tenant);
                }
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsLoadingMore = false;
        }
    }

    public void Receive(IdentityUserCreateMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await GetUsersAsync();
        });
    }

    public void Receive(IdentityUserEditMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await GetUsersAsync();
        });
    }
}
