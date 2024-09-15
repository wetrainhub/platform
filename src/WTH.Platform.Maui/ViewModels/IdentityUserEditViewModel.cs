using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WTH.Platform.Maui.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace WTH.Platform.Maui.ViewModels;

[QueryProperty("UserId", "UserId")]
public partial class IdentityUserEditViewModel : PlatformViewModelBase, ITransientDependency
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

    protected IIdentityUserAppService IdentityUserAppService { get; }

    [ObservableProperty]
    IdentityUserUpdateDto user = new();

    [ObservableProperty]
    public string? userId;

    public IdentityUserEditViewModel(IIdentityUserAppService identityUserAppService)
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

    public async void GetUserAsync()
    {
        IsBusy = true;
        var user = await IdentityUserAppService.GetAsync(Guid.Parse(UserId!));

        User = new IdentityUserUpdateDto
        {
            UserName = user!.UserName,
            Name = user.Name,
            Email = user.Email,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            LockoutEnabled = user.LockoutEnabled,
            IsActive = user.IsActive,
            ConcurrencyStamp = user.ConcurrencyStamp
        };

        IsBusy = false;

        await GetRoles();
    }

    partial void OnUserIdChanged(string? value)
    {
        GetUserAsync();
    }

    [RelayCommand]
    async Task GetRoles()
    {
        try
        {
            IsRolesBusy = true;
            Roles = (await IdentityUserAppService.GetAssignableRolesAsync()).Items;

            if (User is not null)
            {
                var roleNames = (await IdentityUserAppService.GetRolesAsync(Guid.Parse(UserId!))).Items.Select(r => r.Name).ToArray();
                SelectionList = Roles
                    .Select(x => new SelectionViewModel
                    {
                        DisplayName = x.Name,
                        Key = x.Id,
                        IsSelected = roleNames.Contains(x.Name)
                    }).ToArray();
            }
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
    async Task Edit()
    {
        try
        {
            IsBusy = true;
            var userId = Guid.Parse(UserId!);

            await IdentityUserAppService.UpdateAsync(userId, User);

            await IdentityUserAppService.UpdateRolesAsync(userId, new IdentityUserUpdateRolesDto
            {
                RoleNames = SelectionList.Where(x => x.IsSelected).Select(x => x.DisplayName).ToArray()
            });

            WeakReferenceMessenger.Default.Send(new IdentityUserEditMessage(new IdentityUserEditMessageArgs
            {
                User = User,
                UserId = userId
            }));

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsBusy = false;
        }

        WeakReferenceMessenger.Default.Send(new IdentityUserEditMessage(new IdentityUserEditMessageArgs
        {
            UserId = Guid.Parse(UserId!),
            User = User
        }));

        await Shell.Current.GoToAsync("..");
    }
}
