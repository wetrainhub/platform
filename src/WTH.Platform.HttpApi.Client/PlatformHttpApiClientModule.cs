using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.AuditLogging;
using Volo.Abp.LanguageManagement;
using Volo.FileManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict;
using Volo.CmsKit;
using Volo.Chat;
using Wth.Crm;
using WTH.Training;

namespace WTH.Platform;

[DependsOn(
    typeof(PlatformApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpAccountAdminHttpApiClientModule),
    typeof(AbpAccountPublicHttpApiClientModule),
    typeof(SaasHostHttpApiClientModule),
    typeof(AbpAuditLoggingHttpApiClientModule),
    typeof(AbpOpenIddictProHttpApiClientModule),
    typeof(TextTemplateManagementHttpApiClientModule),
    typeof(LanguageManagementHttpApiClientModule),
    typeof(FileManagementHttpApiClientModule),
    typeof(AbpGdprHttpApiClientModule),
    typeof(CmsKitProHttpApiClientModule),
    typeof(ChatHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
    [DependsOn(typeof(CrmHttpApiClientModule))]
    [DependsOn(typeof(TrainingHttpApiClientModule))]
    public class PlatformHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PlatformApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PlatformHttpApiClientModule>();
        });
    }
}
