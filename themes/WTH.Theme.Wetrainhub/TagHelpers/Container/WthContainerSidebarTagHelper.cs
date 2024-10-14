using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Container;

[HtmlTargetElement("wth-container-sidebar",ParentTag = "wth-container")]
public class WthContainerSidebarTagHelper(WthContainerSidebarTagHelperService tagHelperService)
    : AbpTagHelper<WthContainerSidebarTagHelper, WthContainerSidebarTagHelperService>(tagHelperService);