using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace WTH.Training;

[DependsOn(
    typeof(TrainingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class TrainingApplicationContractsModule : AbpModule
{

}
