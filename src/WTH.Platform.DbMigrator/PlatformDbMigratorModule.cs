using WTH.Platform.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace WTH.Platform.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(PlatformApplicationContractsModule)
)]
public class PlatformDbMigratorModule : AbpModule
{
}
