using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Container;

[HtmlTargetElement("wth-container-content",ParentTag = "wth-container")]
public class WthContainerContentTagHelper(WthContainerContentTagHelperService tagHelperService)
    : AbpTagHelper<WthContainerContentTagHelper, WthContainerContentTagHelperService>(tagHelperService);