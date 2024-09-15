using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Wth.Crm;

[DependsOn(
    typeof(CrmApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CrmHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CrmApplicationContractsModule).Assembly,
            CrmRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CrmHttpApiClientModule>();
        });
    }
}
