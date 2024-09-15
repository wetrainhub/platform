using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Pages;

public partial class IdentityUserCreateModalPage : ContentPage, ITransientDependency
{
    public IdentityUserCreateModalPage(IdentityUserCreateViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}