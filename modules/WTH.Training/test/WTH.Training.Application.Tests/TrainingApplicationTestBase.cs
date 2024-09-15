using Volo.Abp.Modularity;

namespace WTH.Training;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class TrainingApplicationTestBase<TStartupModule> : TrainingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
