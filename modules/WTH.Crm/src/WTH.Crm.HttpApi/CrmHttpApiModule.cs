using Localization.Resources.AbpUi;
using Wth.Crm.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Wth.Crm;

[DependsOn(
    typeof(CrmApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CrmHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CrmHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CrmResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
