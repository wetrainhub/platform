using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.AuditLogging;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Azure;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Gdpr;
using Volo.Abp.LanguageManagement;
using Volo.FileManagement;
using Volo.Abp.OpenIddict;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.CmsKit;
using Volo.Chat;
using Wth.Crm;
using WTH.Training;

namespace WTH.Platform;

[DependsOn(
    typeof(PlatformDomainModule),
    typeof(PlatformApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(SaasHostApplicationModule),
    typeof(ChatApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(TextTemplateManagementApplicationModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(FileManagementApplicationModule),
    typeof(AbpGdprApplicationModule),
    typeof(CmsKitProApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBlobStoringAzureModule)
    )]
    [DependsOn(typeof(CrmApplicationModule))]
    [DependsOn(typeof(TrainingApplicationModule))]
    public class PlatformApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {       
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformApplicationModule>();
        });
        
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<FileManagementContainer>(c =>
            {
                c.UseAzure(options =>
                {
                    options.ConnectionString = configuration["Azure:BlobStorage:ConnectionString"];
                    options.ContainerName = "file-management";
                    options.CreateContainerIfNotExists = true;
                }); // You can use FileSystem or Azure providers also.
            });
        });
    }
}
