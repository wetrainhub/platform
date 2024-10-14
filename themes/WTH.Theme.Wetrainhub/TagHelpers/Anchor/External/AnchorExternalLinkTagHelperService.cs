using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.External;

public partial class AnchorExternalLinkTagHelperService : AbpTagHelperService<AnchorExternalLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
       var href = output.Attributes["href"].Value?.ToString() ?? string.Empty;
        if (!HttpRegex().IsMatch(href))
        {
            return;
        }

        output.Attributes.Add("target", "_blank");
        output.Attributes.SetAttribute("rel", "noopener noreferrer"); // Security best practice
        output.AddClass("external-link", HtmlEncoder.Default);
        
        var iconElement = new TagBuilder("i");
        iconElement.AddCssClass("fa fa-external-link ms-2");
        output.PostContent.AppendHtml(iconElement);
        
        if (TagHelper.Text != null)
        {
            output.Content.SetContent(TagHelper.Text);
        }
    }

    [GeneratedRegex("^http")]
    private static partial Regex HttpRegex();
}