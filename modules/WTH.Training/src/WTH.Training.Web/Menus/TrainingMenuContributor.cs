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
            TrainingMenus.Awards, 
            l["Menu:Training:Reminders"], 
            "~/Reminders");
        
        var remindersMenuItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Reminders"], 
            "~/Reminders",
            icon:"fa-bell-ring");
        
        var templatesMenuItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Reminders:Templates"], 
            "~/ReminderTemplates",
            icon:"fa-pen-ruler");

        
        moduleMenu.Items.Add(remindersTabItem);
        remindersTabItem.Items.Add(remindersMenuItem);
        remindersTabItem.Items.Add(templatesMenuItem);
    }

    private static void AddAwards(MenuConfigurationContext context, ApplicationMenuItem moduleMenu)
    {
        var l = context.GetLocalizer<TrainingResource>();
        var awardsTabItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Awards"], 
            "~/Awards");
        
        var awardsMenuItem = new ApplicationMenuItem(
            TrainingMenus.Awards, 
            l["Menu:Training:Awards"], 
            "~/Awards",
            icon:"fa-award");
        
        var awardTypesMenuItem =
            new ApplicationMenuItem(
                TrainingMenus.AwardTypes,
                l["Menu:Training:Awards:Types"], 
                "~/AwardTypes",
                icon: "fa-square-sliders");
        
        var awardingOrganisationsMenuItem = new ApplicationMenuItem(
            TrainingMenus.AwardingOrganisations,
            l["Menu:Training:Awards:Organisations"],
            "~/AwardingOrganisations",
            icon: "fa-building");

        moduleMenu.Items.Add(awardsTabItem);
        awardsTabItem.Items.Add(awardsMenuItem);
        awardsTabItem.Items.Add(awardTypesMenuItem);
        awardsTabItem.Items.Add(awardingOrganisationsMenuItem);
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