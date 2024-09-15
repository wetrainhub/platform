using Volo.Abp.Modularity;

namespace Wth.Crm;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class CrmApplicationTestBase<TStartupModule> : CrmTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
