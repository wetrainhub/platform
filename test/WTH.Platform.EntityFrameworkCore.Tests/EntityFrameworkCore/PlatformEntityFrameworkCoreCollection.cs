using Xunit;

namespace WTH.Platform.EntityFrameworkCore;

[CollectionDefinition(PlatformTestConsts.CollectionDefinitionName)]
public class PlatformEntityFrameworkCoreCollection : ICollectionFixture<PlatformEntityFrameworkCoreFixture>
{

}
