using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WTH.Platform.Maui.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace WTH.Platform.Maui.ViewModels;
public partial class IdentityUserCreateViewModel : PlatformViewModelBase, ITransientDependency
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isRolesBusy;

    [ObservableProperty]
    private bool isUserInformationPageVisible = true;

    [ObservableProperty]
    private bool isRolesTabVisible;

    [ObservableProperty]
    public IReadOnlyList<IdentityRoleDto> roles = Array.Empty<IdentityRoleDto>();

    [ObservableProperty]
    SelectionViewModel[] selectionList = Array.Empty<SelectionViewModel>();

    public IdentityUserCreateDto User { get; } = new();

    protected IIdentityUserAppService IdentityUserAppService { get; }

    public IdentityUserCreateViewModel(IIdentityUserAppService identityUserAppService)
    {
        IdentityUserAppService = identityUserAppService;
    }

    [RelayCommand]
    void ShowRolesTab()
    {
        IsUserInformationPageVisible = false;
        IsRolesTabVisible = true;
    }

    [RelayCommand]
    void ShowUserInformationTab()
    {
        IsUserInformationPageVisible = true;
        IsRolesTabVisible = false;
    }

    [RelayCommand]
    async Task GetRoles()
    {
        try
        {
            IsRolesBusy = true;
            Roles = (await IdentityUserAppService.GetAssignableRolesAsync()).Items;

            SelectionList = Roles.Select(x => new SelectionViewModel
            {
                DisplayName = x.Name,
                IsSelected = false,
                Key = x.Id
            }).ToArray();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsRolesBusy = false;
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
            User.RoleNames = SelectionList.Select(x => x.DisplayName).ToArray();
            await IdentityUserAppService.CreateAsync(User);
            await Shell.Current.GoToAsync("..");
            WeakReferenceMessenger.Default.Send(new IdentityUserCreateMessage(User));
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
