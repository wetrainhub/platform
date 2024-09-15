using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using WTH.Platform.Localization;
using WTH.Theme.Wetrainhub;

namespace WTH.Platform.Web;

[Dependency(ReplaceServices = true)]
public class PlatformBrandingProvider : ThemeBrandingProvider
{
    private IStringLocalizer<PlatformResource> _localizer;

    public PlatformBrandingProvider(IStringLocalizer<PlatformResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
    public override string LogoUrl => "/img/logo-header.png";
    public override string LoginLogoUrl => "/img/logo-login.png";
    public override string? LoginBackgroundUrl => "/img/background-login.png";
}
