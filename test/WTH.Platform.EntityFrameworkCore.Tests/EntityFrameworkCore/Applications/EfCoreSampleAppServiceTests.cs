using WTH.Platform.Samples;
using Xunit;

namespace WTH.Platform.EntityFrameworkCore.Applications;

[Collection(PlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<PlatformEntityFrameworkCoreTestModule>
{

}
