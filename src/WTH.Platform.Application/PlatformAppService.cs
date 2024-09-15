using WTH.Platform.Localization;
using Volo.Abp.Application.Services;

namespace WTH.Platform;

/* Inherit your application services from this class.
 */
public abstract class PlatformAppService : ApplicationService
{
    protected PlatformAppService()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}
