using Volo.Abp.Modularity;

namespace WTH.Training;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class TrainingDomainTestBase<TStartupModule> : TrainingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
