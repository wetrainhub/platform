using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace WTH.Theme.Wetrainhub.Bundling;

public class WetrainhubThemeGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Clear();
        
        context.Files.Add("/themes/wetrainhub/plugins/global/plugins.bundle.js");
        context.Files.Add("/themes/wetrainhub/js/scripts.bundle.js");
    }
}
