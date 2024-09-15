using Volo.Abp.Ui.Branding;

namespace WTH.Theme.Wetrainhub;

public interface IThemeBrandingProvider : IBrandingProvider
{
    string? LoginLogoUrl { get; }
    string? LoginBackgroundUrl { get; }
}