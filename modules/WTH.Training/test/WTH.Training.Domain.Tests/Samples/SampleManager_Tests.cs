using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace WTH.Training.Samples;

public abstract class SampleManager_Tests<TStartupModule> : TrainingDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    //private readonly SampleManager _sampleManager;

    protected SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}
