using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace WTH.Theme.Wetrainhub;

[ThemeName(Name)]
public class WetrainhubTheme : ITheme, ITransientDependency
{
    public const string Name = "Wetrainhub";

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
                return "~/Themes/Wetrainhub/Layouts/Application.cshtml";
            case StandardLayouts.Account:
                return "~/Themes/Wetrainhub/Layouts/Account.cshtml";
            case StandardLayouts.Empty:
                return "~/Themes/Wetrainhub/Layouts/Empty.cshtml";
            default:
                return fallbackToDefault ? "~/Themes/Wetrainhub/Layouts/Application.cshtml" : null;
        }
    }
}
