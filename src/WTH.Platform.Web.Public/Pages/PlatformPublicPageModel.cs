using WTH.Platform.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WTH.Platform.Web.Public.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class PlatformPublicPageModel : AbpPageModel
{
    protected PlatformPublicPageModel()
    {
        LocalizationResourceType = typeof(PlatformResource);
    }
}
