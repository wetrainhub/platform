using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using WTH.Platform.Localization;
using WTH.Platform.Permissions;
using WTH.Platform.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.CmsKit.Pro.Admin.Web.Menus;
using Volo.Saas.Host.Navigation;

namespace WTH.Platform.Web.Menus;

public class PlatformMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<PlatformResource>();

        //Dashboards
        AddDashboards(context, l);


        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                PlatformMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(PlatformPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                PlatformMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(PlatformPermissions.Dashboard.Tenant)
        );
    
        //CMS
        context.Menu.SetSubItemOrder(CmsKitProAdminMenus.GroupName, 4);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->Saas
        administration.SetSubItemOrder(SaasHostMenuNames.GroupName, 2);

        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 3);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 4);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 5);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 6);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }

    private static void AddDashboards(MenuConfigurationContext context, IStringLocalizer l)
    {
        var dashboards = new ApplicationMenuItem(
            PlatformMenus.Dashboards,
            l["Menu:Dashboards"],
            "~/",
            icon: "fa fa-home",
            order: 1
        );
        
        dashboards.AddItem(
            new ApplicationMenuItem(
                PlatformMenus.Dashboard,
                l["Menu:Dashboard"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );
     
        context.Menu.AddItem(dashboards);
    }
}
