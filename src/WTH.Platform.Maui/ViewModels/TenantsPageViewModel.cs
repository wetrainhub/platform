using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Saas.Host;
using Volo.Saas.Host.Dtos;
using WTH.Platform.Maui.Pages;
using CommunityToolkit.Mvvm.Messaging;
using WTH.Platform.Maui.Messages;
using Volo.Abp.Threading;

namespace WTH.Platform.Maui.ViewModels;

public partial class TenantsPageViewModel : PlatformViewModelBase,
    IRecipient<TenantCreateMessage>, //create
    IRecipient<TenantEditMessage>, //create
    ITransientDependency
{
    public ObservableCollection<SaasTenantDto> Items { get; } = new();

    public GetTenantsInput Input { get; } = new();

    protected ITenantAppService TenantAppService { get; }

    protected SemaphoreSlim SemaphoreSlim { get; } = new SemaphoreSlim(1, 1);

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    bool isLoadingMore;

    [ObservableProperty]
    bool canLoadMore = true;

    public TenantsPageViewModel(
        ITenantAppService tenantAppService)
    {
        TenantAppService = tenantAppService;

        WeakReferenceMessenger.Default.Register<TenantCreateMessage>(this);
        WeakReferenceMessenger.Default.Register<TenantEditMessage>(this);
    }

    [RelayCommand]
    async Task OpenCreateModal()
    {
        await Shell.Current.GoToAsync(nameof(TenantCreatePage));
    }

    [RelayCommand]
    async Task OpenEditModal(Guid id)
    {
        await Shell.Current.GoToAsync($"{nameof(TenantEditPage)}?Id={id}");
    }

    [RelayCommand]
    async Task Refresh()
    {
        try
        {
            IsBusy = true;

            using (await SemaphoreSlim.LockAsync())
            {
                Input.SkipCount = 0;
                var tenants = await TenantAppService.GetListAsync(Input);
                Items.Clear();
                foreach (var tenant in tenants.Items)
                {
                    Items.Add(tenant);
                }
                CanLoadMore = tenants.Items.Count >= Input.MaxResultCount;
            }
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

                var tenants = await TenantAppService.GetListAsync(Input);

                CanLoadMore = tenants.Items.Count >= Input.MaxResultCount;

                foreach (var tenant in tenants.Items)
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

    [RelayCommand]
    async Task ShowActions(SaasTenantDto entity)
    {
        var result = await App.Current!.MainPage!.DisplayActionSheet(
            L["Actions"],
            L["Cancel"],
            null,
            L["Edit"], L["Delete"]);

        if (result == L["Edit"])
        {
            await OpenEditModal(entity.Id);
        }

        if (result == L["Delete"])
        {
            await Delete(entity);
        }
    }

    [RelayCommand]
    async Task Delete(SaasTenantDto entity)
    {
        if (Application.Current is { MainPage: { } })
        {
            var confirmed = await Shell.Current.CurrentPage.DisplayAlert(
                L["Delete"],
                string.Format(L["TenantDeletionConfirmationMessage"], entity.Name),
                L["Delete"],
                L["Cancel"]);

            if (!confirmed)
            {
                return;
            }

            try
            {
                await TenantAppService.DeleteAsync(entity.Id);
            }
            catch (AbpRemoteCallException remoteException)
            {
                HandleException(remoteException);
            }

            await Refresh();
        }
    }

    public async void Receive(TenantCreateMessage message)
    {
        await Refresh();
    }

    public async void Receive(TenantEditMessage message)
    {
        await Refresh();
    }
}