using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Pages;

public partial class IdentityUserPage : ContentPage, ITransientDependency
{
	public IdentityUserPage(IdentityUserPageViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
	}
}