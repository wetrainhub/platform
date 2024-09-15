using Volo.Abp.Settings;

namespace WTH.Platform.Settings;

public class PlatformSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(PlatformSettings.MySetting1));
    }
}
