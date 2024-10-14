using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace WTH.Theme.Wetrainhub.Bundling;

public class WetrainhubThemeGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        var removeFiles = Array.Empty<string>();

        foreach (var file in removeFiles)
        {
            context.Files.RemoveAll(w => w.FileName.Contains(file));
        }

        context.Files.Add("/themes/wetrainhub/plugins/global/plugins.bundle.js");
        context.Files.Add("/themes/wetrainhub/js/scripts.bundle.js");
        context.Files.Add("/themes/wetrainhub/js/tag-helpers.js");
    }
}