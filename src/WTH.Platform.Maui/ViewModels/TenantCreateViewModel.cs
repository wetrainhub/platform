using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WTH.Platform.Maui.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Saas;
using Volo.Saas.Host;
using Volo.Saas.Host.Dtos;

namespace WTH.Platform.Maui.ViewModels;
public partial class TenantCreateViewModel : PlatformViewModelBase, ITransientDependency
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private ObservableCollection<EditionLookupDto> editions = default!;

    [ObservableProperty]
    private EditionLookupDto selectedEdition = default!;
    
    [ObservableProperty]
    private bool isActivationEndDateVisible;

    public SaasTenantCreateDto Tenant { get; set; } = new();

    public TenantActivationState[] ActivationStates { get; } = new[]
    {
        TenantActivationState.Active,
        TenantActivationState.ActiveWithLimitedTime,
        TenantActivationState.Passive,
    };

    protected ITenantAppService TenantAppService { get; }

    public TenantCreateViewModel(ITenantAppService tenantAppService)
    {
        TenantAppService = tenantAppService;
    }

    [RelayCommand]
    async Task GetEditions()
    {
        try
        {
            Editions = new (await TenantAppService.GetEditionLookupAsync());
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Create()
    {
        try
        {
            IsBusy = true;
            Tenant.EditionId = SelectedEdition?.Id;

            await TenantAppService.CreateAsync(Tenant);
            await Shell.Current.GoToAsync("..");
            WeakReferenceMessenger.Default.Send(new TenantCreateMessage(Tenant));
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
