using Volo.Abp.Modularity;

namespace Wth.Crm;

[DependsOn(
    typeof(CrmApplicationModule),
    typeof(CrmDomainTestModule)
    )]
public class CrmApplicationTestModule : AbpModule
{

}
