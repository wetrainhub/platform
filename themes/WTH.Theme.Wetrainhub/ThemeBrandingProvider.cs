using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace WTH.Theme.Wetrainhub;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IThemeBrandingProvider))]
public class ThemeBrandingProvider : DefaultBrandingProvider, IThemeBrandingProvider
{
    public virtual string? LoginLogoUrl => null;
    public virtual string? LoginBackgroundUrl => null;
}