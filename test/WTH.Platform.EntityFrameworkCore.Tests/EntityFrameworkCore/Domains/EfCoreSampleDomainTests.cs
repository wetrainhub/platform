using WTH.Platform.Samples;
using Xunit;

namespace WTH.Platform.EntityFrameworkCore.Domains;

[Collection(PlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<PlatformEntityFrameworkCoreTestModule>
{

}
