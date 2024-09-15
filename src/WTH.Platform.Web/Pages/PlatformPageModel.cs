using WTH.Platform.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WTH.Platform.Web.Pages;

public abstract class PlatformPageModel : AbpPageModel
{
    protected PlatformPageModel()
    {
        LocalizationResourceType = typeof(PlatformResource);
    }
}
