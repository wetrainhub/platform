using Microsoft.VisualBasic;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Linq;
using System.Reflection;

public class WthTableTagHelper(WthTableTagHelperService tagHelperService)
    : AbpTagHelper<WthTableTagHelper, WthTableTagHelperService>(tagHelperService)
{

    public IEnumerable AspItems { get; set; } = new Collection();

}