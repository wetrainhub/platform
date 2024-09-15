using Volo.Abp.Modularity;

namespace Wth.Crm;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class CrmDomainTestBase<TStartupModule> : CrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
