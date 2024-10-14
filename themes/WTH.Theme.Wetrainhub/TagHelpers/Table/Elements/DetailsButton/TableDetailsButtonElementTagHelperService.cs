using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.DetailsButton;

public class TableDetailsButtonElementTagHelperService : AbpTagHelperService<TableDetailsButtonElementTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Url.IsNullOrEmpty())
        {
            return;
        }
        
        output.TagName = "a";
        output.Attributes.AddClass("btn btn-light btn-active-light-primary btn-center btn-sm btn-icon");
        output.Attributes.Add("href", TagHelper.Url);

        var iconElement = new TagBuilder("i");
        iconElement.AddCssClass("fa fa-magnifying-glass");
        output.Content.SetHtmlContent(iconElement);
    }
}