using CommunityToolkit.Mvvm.Input;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.ViewModels;

public partial class MainPageViewModel : PlatformViewModelBase, ITransientDependency
{
    public MainPageViewModel()
    {
    }

    [RelayCommand]
    async Task SeeAllUsers()
    {
        await Shell.Current.GoToAsync("///users");
    }
}
