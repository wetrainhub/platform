using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace WTH.Training;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TrainingHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class TrainingConsoleApiClientModule : AbpModule
{

}
