using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.Footer;

public class FooterViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Wetrainhub/Components/Footer/Default.cshtml");
    }
}
