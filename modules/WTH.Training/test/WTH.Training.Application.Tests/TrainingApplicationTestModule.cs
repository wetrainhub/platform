using Volo.Abp.Modularity;

namespace WTH.Training;

[DependsOn(
    typeof(TrainingApplicationModule),
    typeof(TrainingDomainTestModule)
    )]
public class TrainingApplicationTestModule : AbpModule
{

}
