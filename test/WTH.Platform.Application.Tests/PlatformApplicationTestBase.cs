using Volo.Abp.Modularity;

namespace WTH.Platform;

public abstract class PlatformApplicationTestBase<TStartupModule> : PlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
