using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Pages;

public partial class SettingsPage : ContentPage, ITransientDependency
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}