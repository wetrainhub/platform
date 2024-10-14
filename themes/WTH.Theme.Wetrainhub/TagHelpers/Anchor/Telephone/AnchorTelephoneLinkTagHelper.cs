using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.Telephone;

[HtmlTargetElement("a", Attributes = "href")]
public class AnchorTelephoneLinkTagHelper(AnchorTelephoneLinkTagHelperService elementTagHelperService)
    : AbpTagHelper<AnchorTelephoneLinkTagHelper, AnchorTelephoneLinkTagHelperService>(elementTagHelperService)
{
    public string? Text { get; set; }
}