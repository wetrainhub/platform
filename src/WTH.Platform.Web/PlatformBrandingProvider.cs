using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using WTH.Platform.Localization;

namespace WTH.Platform.Web;

[Dependency(ReplaceServices = true)]
public class PlatformBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PlatformResource> _localizer;

    public PlatformBrandingProvider(IStringLocalizer<PlatformResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
    public override string LogoUrl => "/img/logo-header.png";
}
