using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui;

public partial class MainPage : ContentPage, ITransientDependency
{
    public MainPage(
		MainPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}