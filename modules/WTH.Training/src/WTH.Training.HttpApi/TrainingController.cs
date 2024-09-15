using WTH.Training.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WTH.Training;

public abstract class TrainingController : AbpControllerBase
{
    protected TrainingController()
    {
        LocalizationResource = typeof(TrainingResource);
    }
}
