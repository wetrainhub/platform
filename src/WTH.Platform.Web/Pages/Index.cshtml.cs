using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;
using WTH.Platform.Web.Menus;
using WTH.Theme.Wetrainhub.Layout;

namespace WTH.Platform.Web.Pages;

public class IndexModel(IThemePageLayout themePageLayout) : PlatformPageModel
{
    public void OnGet()
    {
        var content = themePageLayout.Content;
        content.SetMenu(PlatformMenus.Dashboards, PlatformMenus.Dashboard);
        content.SetTitleWithDescription("Test","Dashboard");
       content.SetTitleWithBreadCrumb("Dashboards", new List<BreadCrumbItem>()
       {
              new BreadCrumbItem("Home", "/"),
              new BreadCrumbItem("Dashboards", "/Dashboards")
       });
    }
}