using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.Brand;

public class BrandViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Wetrainhub/Components/Brand/Default.cshtml");
    }
}
