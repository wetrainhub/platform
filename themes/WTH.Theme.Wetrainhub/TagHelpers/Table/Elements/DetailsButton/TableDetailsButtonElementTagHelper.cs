using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.DetailsButton;

public class TableDetailsButtonElementTagHelper(TableDetailsButtonElementTagHelperService elementTagHelperService)
    : AbpTagHelper<TableDetailsButtonElementTagHelper, TableDetailsButtonElementTagHelperService>(elementTagHelperService)
{
    public string Url { get; set; }
}