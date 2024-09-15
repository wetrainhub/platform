using Wth.Crm.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wth.Crm;

public abstract class CrmController : AbpControllerBase
{
    protected CrmController()
    {
        LocalizationResource = typeof(CrmResource);
    }
}
