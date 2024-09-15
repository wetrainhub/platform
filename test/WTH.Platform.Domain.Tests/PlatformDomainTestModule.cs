using Volo.Abp.Modularity;

namespace WTH.Platform;

[DependsOn(
    typeof(PlatformDomainModule),
    typeof(PlatformTestBaseModule)
)]
public class PlatformDomainTestModule : AbpModule
{

}
