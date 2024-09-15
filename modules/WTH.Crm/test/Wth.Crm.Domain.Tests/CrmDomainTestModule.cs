using Volo.Abp.Modularity;

namespace Wth.Crm;

[DependsOn(
    typeof(CrmDomainModule),
    typeof(CrmTestBaseModule)
)]
public class CrmDomainTestModule : AbpModule
{

}
