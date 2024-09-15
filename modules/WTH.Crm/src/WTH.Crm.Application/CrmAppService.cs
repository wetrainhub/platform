using Wth.Crm.Localization;
using Volo.Abp.Application.Services;

namespace Wth.Crm;

public abstract class CrmAppService : ApplicationService
{
    protected CrmAppService()
    {
        LocalizationResource = typeof(CrmResource);
        ObjectMapperContext = typeof(CrmApplicationModule);
    }
}
