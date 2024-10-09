using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Wth.Crm.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Wth.Crm.Web.Menus;

public class CrmMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!
        
        AddCompanies(context, moduleMenu);
        AddEmployees(context, moduleMenu);
    }

    private void AddEmployees(MenuConfigurationContext context, ApplicationMenuItem moduleMenu)
    {
        var l = context.GetLocalizer<CrmResource>();
        
        var employeesMenuItem = new ApplicationMenuItem(
            CrmMenus.Employees,
            displayName: l["Menu:Crm:Employees"],
            "~/Crm/Employees",
            icon: "fa-users");
        
        moduleMenu.Items.Add(employeesMenuItem);
    }

    private void AddCompanies(MenuConfigurationContext context, ApplicationMenuItem moduleMenu)
    {
        var l = context.GetLocalizer<CrmResource>();
        
        var employeesMenuItem = new ApplicationMenuItem(
            CrmMenus.Companies,
            displayName: l["Menu:Crm:Companies"],
            "~/Crm/Companies",
            icon: "fa-building");
        
        moduleMenu.Items.Add(employeesMenuItem);    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CrmResource>();
        
        var moduleMenu = new ApplicationMenuItem(
            CrmMenus.Prefix,
            displayName: l["Menu:Crm"],
            "~/Crm",
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
}