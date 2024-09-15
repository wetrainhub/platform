using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using WTH.Theme.Wetrainhub.Layout;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.ContentTitle;

public class ContentTitleViewComponent(IThemePageLayout themePageLayout) : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        var hasBreadCrumb = themePageLayout.Content.BreadCrumb.Items.Count != 0;
        var view = hasBreadCrumb ? "Breadcrumb" : "Default";
        
        return View($"~/Themes/Wetrainhub/Components/ContentTitle/{view}.cshtml", themePageLayout.Content);
    }
}
