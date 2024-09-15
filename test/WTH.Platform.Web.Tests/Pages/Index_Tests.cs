using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace WTH.Platform.Pages;

[Collection(PlatformTestConsts.CollectionDefinitionName)]
public class Index_Tests : PlatformWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
