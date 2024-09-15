using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace WTH.Training;

[DependsOn(
    typeof(TrainingApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TrainingHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TrainingApplicationContractsModule).Assembly,
            TrainingRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TrainingHttpApiClientModule>();
        });
    }
}
