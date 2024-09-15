using Volo.Abp.Modularity;

namespace WTH.Platform;

/* Inherit from this class for your domain layer tests. */
public abstract class PlatformDomainTestBase<TStartupModule> : PlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
