using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace WTH.Training;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(TrainingDomainSharedModule)
)]
public class TrainingDomainModule : AbpModule
{

}
