using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.Header;

public class HeaderViewComponent(IMenuManager menuManager) : AbpViewComponent
{
    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var tabMenu = await menuManager.GetMainMenuAsync();
        return View("~/Themes/Wetrainhub/Components/Header/Default.cshtml", tabMenu);
    }
}