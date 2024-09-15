using WTH.Training.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WTH.Training.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TrainingPageModel : AbpPageModel
{
    protected TrainingPageModel()
    {
        LocalizationResourceType = typeof(TrainingResource);
        ObjectMapperContext = typeof(TrainingWebModule);
    }
}
