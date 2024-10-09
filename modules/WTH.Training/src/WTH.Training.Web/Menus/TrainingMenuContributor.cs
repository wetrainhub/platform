using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using WTH.Training.Localization;
using Volo.Abp.Authorization.Permissions;

namespace WTH.Training.Web.Menus;

public class TrainingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu =
            AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddAwards(context, moduleMenu);
        AddReminders(context, moduleMenu);
    }

    private void AddReminders(MenuConfigurationContext context, ApplicationMenuItem moduleMenu)
    {
        var l = context.GetLocalizer<TrainingResource>();
        var remindersTabItem = new ApplicationMenuItem(
            TrainingMenus.Reminders, 
            l["Menu:Training:Reminders"], 
            "~/Training/Reminders");
        
        var logsMenuItem = new ApplicationMenuItem(
            TrainingMenus.Reminders, 
            l["Menu:Training:Reminders:Logs"], 
            "~/Training/Reminders/Logs",
            icon:"fa-bell-ring");
        
        var templatesMenuItem = new ApplicationMenuItem(
            TrainingMenus.RemindersTemplates, 
            l["Menu:Training:Reminders:Templates"], 
            "~/Training/Reminders/Templates",
            icon:"fa-pen-ruler");

        
        moduleMenu.Items.Add(remindersTabItem);
        remindersTabItem.Items.Add(logsMenuItem);
        remindersTabItem.Items.Add(templatesMenuItem);
    }

    private static void AddAwards(MenuConfigurationContext context, ApplicationMenuItem moduleMenu)
    {
        var l = context.GetLocalizer<TrainingResource>();
        var certificateMenuItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Certification"], 
            "~/Training/Certification");
        
        var awardsMenuItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Certification:Awards"], 
            "~/Training/Awards",
            icon:"fa-award");
        
        var awardTypesMenuItem =
            new ApplicationMenuItem(
                TrainingMenus.AwardTypes,
                l["Menu:Training:Certification:Types"], 
                "~/Training/AwardTypes",
                icon: "fa-square-sliders");
        
        var awardingOrganisationsMenuItem = new ApplicationMenuItem(
            TrainingMenus.AwardingOrganisations,
            l["Menu:Training:Certification:Organisations"],
            "~/Training/AwardingOrganisations",
            icon: "fa-building");

        moduleMenu.Items.Add(certificateMenuItem);
        certificateMenuItem.Items.Add(awardsMenuItem);
        certificateMenuItem.Items.Add(awardTypesMenuItem);
        certificateMenuItem.Items.Add(awardingOrganisationsMenuItem);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<TrainingResource>();

        var moduleMenu = new ApplicationMenuItem(
            TrainingMenus.Prefix,
            displayName: l["Menu:Training"],
            "~/Training",
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
}