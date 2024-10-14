using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.Telephone;

public partial class AnchorTelephoneLinkTagHelperService : AbpTagHelperService<AnchorTelephoneLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
       var href = output.Attributes["href"].Value?.ToString() ?? string.Empty;
        if (!TelephoneRegex().IsMatch(href) && !href.StartsWith("tel:"))
        {
            return;
        }

        output.AddClass("telephone-link", HtmlEncoder.Default);
        var cleanHref = href.Replace("tel:",string.Empty);
        output.Attributes.SetAttribute("href", $"tel:{cleanHref}");
        
        var iconElement = new TagBuilder("i");
        iconElement.AddCssClass("fa fa-phone me-2");
        output.PreContent.AppendHtml(iconElement);
        
        if (TagHelper.Text != null)
        {
            output.Content.SetContent(TagHelper.Text);
        }
    } 

    [GeneratedRegex(@"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")]
    private static partial Regex TelephoneRegex();
}