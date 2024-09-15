using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;

namespace WTH.Platform.Maui.ViewModels;

public partial class ChangePasswordViewModel : PlatformViewModelBase, ITransientDependency
{
    [ObservableProperty] bool isBusy;

    protected IProfileAppService ProfileAppService { get; }

    public ChangePasswordViewModel(IProfileAppService profileAppService)
    {
        ProfileAppService = profileAppService;
    }

    [ObservableProperty]
    [property: Required]
    string currentPassword = default!;

    [ObservableProperty]
    [property: Required]
    string newPassword = default!;

    [ObservableProperty]
    [property: Required]
    string newPasswordConfirm = default!;

    public string New { get; set; } = default!;

    [RelayCommand]
    async Task ChangePassword()
    {
        IsBusy = true;

        try
        {
            await ProfileAppService.ChangePasswordAsync(new ChangePasswordInput
            {
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword
            });

            var toast = Toast.Make(L["PasswordChangedMessage"], ToastDuration.Short);

            await toast.Show();
        }
        catch (AbpRemoteCallException ex)
        {
            HandleException(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
