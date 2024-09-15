using Volo.Abp.Modularity;

namespace WTH.Platform;

[DependsOn(
    typeof(PlatformApplicationModule),
    typeof(PlatformDomainTestModule)
)]
public class PlatformApplicationTestModule : AbpModule
{

}
