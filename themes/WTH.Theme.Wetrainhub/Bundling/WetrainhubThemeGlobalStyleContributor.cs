using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace WTH.Theme.Wetrainhub.Bundling;

public class WetrainhubThemeGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Clear();

        context.Files.Add("/themes/wetrainhub/css/style.bundle.css");
        context.Files.Add("/themes/wetrainhub/plugins/global/plugins.bundle.css");
        context.Files.Add("/themes/wetrainhub/css/wetrainhub.css");
        
        context.Files.Add("/themes/wetrainhub/fontawesome/css/fontawesome.css");
        context.Files.Add("/themes/wetrainhub/fontawesome/css/light.css");
        context.Files.Add("/themes/wetrainhub/fontawesome/css/brands.css");
    }
}