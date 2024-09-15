using Volo.Abp.Modularity;

namespace WTH.Training;

[DependsOn(
    typeof(TrainingDomainModule),
    typeof(TrainingTestBaseModule)
)]
public class TrainingDomainTestModule : AbpModule
{

}
