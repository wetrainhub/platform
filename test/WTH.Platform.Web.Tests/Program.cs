using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;
using WTH.Platform;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<PlatformWebTestModule>();

namespace WTH.Platform
{
    public partial class Program
    {
    }
}
