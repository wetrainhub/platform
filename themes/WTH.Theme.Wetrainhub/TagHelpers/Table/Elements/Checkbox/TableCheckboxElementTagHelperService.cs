using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Fields;

public class TableCheckboxElementTagHelperService : AbpTagHelperService<TableCheckboxElementTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("form-check form-check-sm form-check-custom form-check-solid");

        var checkboxElement = new TagBuilder("input");
        checkboxElement.AddCssClass("form-check-input");
        checkboxElement.Attributes.Add("type", "checkbox");
        checkboxElement.Attributes.Add("value", TagHelper.Value?.ToString() ?? string.Empty);

        if (TagHelper.Checked)
        {
            checkboxElement.Attributes.Add("checked", "checked");
        }

        output.Content.SetHtmlContent(checkboxElement);
    }
}