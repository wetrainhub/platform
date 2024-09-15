using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Wth.Crm;

[DependsOn(
    typeof(CrmDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class CrmApplicationContractsModule : AbpModule
{

}
