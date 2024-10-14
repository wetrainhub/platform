using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.External;

[HtmlTargetElement("a", Attributes = "href")]
public class AnchorExternalLinkTagHelper(AnchorExternalLinkTagHelperService elementTagHelperService)
    : AbpTagHelper<AnchorExternalLinkTagHelper, AnchorExternalLinkTagHelperService>(elementTagHelperService)
{
    public string? Text { get; set; }
}