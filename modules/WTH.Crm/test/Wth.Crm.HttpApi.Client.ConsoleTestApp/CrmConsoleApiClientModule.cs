using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Wth.Crm;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CrmHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CrmConsoleApiClientModule : AbpModule
{

}
