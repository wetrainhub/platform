using Localization.Resources.AbpUi;
using WTH.Training.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace WTH.Training;

[DependsOn(
    typeof(TrainingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TrainingHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TrainingHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TrainingResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
