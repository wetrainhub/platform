using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.MainNavbar;

public class MainNavbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Wetrainhub/Components/MainNavbar/Default.cshtml");
    }
}
