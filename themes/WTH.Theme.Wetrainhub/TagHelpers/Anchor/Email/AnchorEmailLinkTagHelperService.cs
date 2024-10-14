using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Anchor.Email;

public partial class AnchorEmailLinkTagHelperService : AbpTagHelperService<AnchorEmailLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var href = output.Attributes["href"].Value?.ToString() ?? string.Empty;
        if (!EmailRegex().IsMatch(href) && !href.StartsWith("mailto:"))
        {
            return;
        }

        output.AddClass("email-link", HtmlEncoder.Default);
        var cleanHref = href.Replace("mailto:",string.Empty);
        output.Attributes.SetAttribute("href", $"mailto:{cleanHref}");

        var iconElement = new TagBuilder("i");
        iconElement.AddCssClass("fa fa-envelope me-2");
        output.PreContent.AppendHtml(iconElement);

        if (TagHelper.Text != null)
        {
            output.Content.SetContent(TagHelper.Text);
        }
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}