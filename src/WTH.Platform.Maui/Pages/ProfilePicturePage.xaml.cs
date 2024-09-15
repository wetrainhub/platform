using WTH.Platform.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Pages;

public partial class ProfilePicturePage : ContentPage, ITransientDependency
{
    public ProfilePicturePage(ProfilePictureViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}