using WTH.Training.Localization;
using Volo.Abp.Application.Services;

namespace WTH.Training;

public abstract class TrainingAppService : ApplicationService
{
    protected TrainingAppService()
    {
        LocalizationResource = typeof(TrainingResource);
        ObjectMapperContext = typeof(TrainingApplicationModule);
    }
}
