using System.Collections.Generic;
using Volo.Abp.Localization;

namespace WTH.Theme.Wetrainhub.Themes.Wetrainhub.Components.Toolbar.LanguageSwitch;

public class LanguageSwitchViewComponentModel
{
    public LanguageInfo CurrentLanguage { get; set; }

    public List<LanguageInfo> OtherLanguages { get; set; }
}
