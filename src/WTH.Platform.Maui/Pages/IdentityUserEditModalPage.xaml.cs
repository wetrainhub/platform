using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Pages;

public partial class IdentityUserEditModalPage : ContentPage, ITransientDependency
{
	public IdentityUserEditModalPage(IdentityUserEditViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}