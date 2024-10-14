using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.Email;

[HtmlTargetElement("a", Attributes = "href")]
public class AnchorEmailLinkTagHelper(AnchorEmailLinkTagHelperService elementTagHelperService)
    : AbpTagHelper<AnchorEmailLinkTagHelper, AnchorEmailLinkTagHelperService>(elementTagHelperService)
{
    public string? Text { get; set; }
}