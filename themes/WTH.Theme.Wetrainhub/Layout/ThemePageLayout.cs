using Volo.Abp.DependencyInjection;

namespace WTH.Theme.Wetrainhub.Layout;

public class ThemePageLayout : IThemePageLayout, IScopedDependency
{
    public ThemeContentLayout Content { get; }
    public ThemeMetaLayout Meta { get; } 

    public ThemePageLayout()
    {
        Content = new ThemeContentLayout();
        Meta = new ThemeMetaLayout();
    }
}