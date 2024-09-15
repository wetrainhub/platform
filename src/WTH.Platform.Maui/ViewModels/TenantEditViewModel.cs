using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WTH.Platform.Maui.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectExtending;
using Volo.Saas;
using Volo.Saas.Host;
using Volo.Saas.Host.Dtos;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace WTH.Platform.Maui.ViewModels;
[QueryProperty("Id", "Id")]
public partial class TenantEditViewModel : PlatformViewModelBase, ITransientDependency
{
    [ObservableProperty]
    public string? id;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isSaving;

    [ObservableProperty]
    private ObservableCollection<EditionLookupDto> editions = new();

    [ObservableProperty]
    private EditionLookupDto? selectedEdition;

    [ObservableProperty]
    private bool isActivationEndDateVisible;

    [ObservableProperty]
    private SaasTenantUpdateDto? tenant;

    public TenantActivationState[] ActivationStates { get; } = new[]
    {
        TenantActivationState.Active,
        TenantActivationState.ActiveWithLimitedTime,
        TenantActivationState.Passive,
    };

    protected ITenantAppService TenantAppService { get; }

    public TenantEditViewModel(ITenantAppService tenantAppService)
    {
        TenantAppService = tenantAppService;
    }

    async partial void OnIdChanged(string? value)
    {
        IsBusy = true;
        await GetTenant();
        await GetEditions();
        IsBusy = false;
    }

    [RelayCommand]
    async Task GetTenant()
    {
        try
        {
            var tenantId = Guid.Parse(Id!);
            var tenantDto = await TenantAppService.GetAsync(tenantId);

            Tenant = new SaasTenantUpdateDto
            {
                ActivationEndDate = tenantDto.ActivationEndDate,
                EditionId = tenantDto.EditionId,
                ActivationState = tenantDto.ActivationState,
                ConcurrencyStamp = tenantDto.ConcurrencyStamp,
                EditionEndDateUtc = tenantDto.EditionEndDateUtc,
                Name = tenantDto.Name
            };

            tenantDto.MapExtraPropertiesTo(Tenant);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    [RelayCommand]
    async Task GetEditions()
    {
        try
        {
            Editions = new(await TenantAppService.GetEditionLookupAsync());
            SelectedEdition = Editions.FirstOrDefault(x => x.Id == Tenant?.EditionId);
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
    async Task Update()
    {
        try
        {
            IsSaving = true;
            Tenant!.EditionId = SelectedEdition?.Id;

            await TenantAppService.UpdateAsync(Guid.Parse(Id!), Tenant);
            await Shell.Current.GoToAsync("..");
            WeakReferenceMessenger.Default.Send(new TenantEditMessage(Tenant));
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsSaving = false;
        }
    }
}
