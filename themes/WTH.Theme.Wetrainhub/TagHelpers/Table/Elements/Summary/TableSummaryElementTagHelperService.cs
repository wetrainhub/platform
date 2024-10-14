using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using YamlDotNet.Core.Tokens;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.Summary;

public class TableSummaryElementTagHelperService : AbpTagHelperService<TableSummaryElementTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("d-flex align-items-center");

        SetImage(output);
        SetLines(output);
    }

    private void SetLines(TagHelperOutput output)
    {
        var containerElement = new TagBuilder("div");
        containerElement.AddCssClass("d-flex flex-column");

        var hasUrl = !TagHelper.Url.IsNullOrEmpty();
        var line1ElementTagName = hasUrl ? "a" : "span";
        var line1Element = new TagBuilder(line1ElementTagName);
        var hoverCssClass = hasUrl ? "text-hover-primary" : string.Empty;
        line1Element.AddCssClass($"text-gray-800 {hoverCssClass} mb-1");
        line1Element.InnerHtml.SetHtmlContent(TagHelper.Line1Text);

        if (hasUrl)
        {
            line1Element.Attributes.Add("href",TagHelper.Url);
        }
        
        containerElement.InnerHtml.AppendHtml(line1Element);

        var line2Element = new TagBuilder("span");
        line2Element.InnerHtml.SetHtmlContent(TagHelper.Line2Text?? string.Empty);
        containerElement.InnerHtml.AppendHtml(line2Element);
        
        output.Content.AppendHtml(containerElement);
    }

    private void SetImage(TagHelperOutput output)
    {
        var containerElement = new TagBuilder("div");
        containerElement.AddCssClass("symbol symbol-circle symbol-50px overflow-hidden me-3");

        var linkElement = new TagBuilder("a");
        linkElement.Attributes.Add("href",TagHelper.Url);
        
        var symbolElement = new TagBuilder("div");
        symbolElement.AddCssClass("symbol-label");

        if (TagHelper.Url.IsNullOrEmpty())
        {
            containerElement.InnerHtml.SetHtmlContent(symbolElement);
        }
        else
        {
            linkElement.InnerHtml.SetHtmlContent(symbolElement);
            containerElement.InnerHtml.SetHtmlContent(linkElement);
        }

        var imageElement = new TagBuilder("img");
        imageElement.AddCssClass("w-100");
        imageElement.Attributes.Add("alt", TagHelper.Line1Text);
        imageElement.Attributes.Add("src", TagHelper.ImageUrl);
        symbolElement.InnerHtml.SetHtmlContent(imageElement);

        output.Content.AppendHtml(containerElement);
    }
}