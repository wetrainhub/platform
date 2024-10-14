using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.Summary;

public class TableSummaryElementTagHelper(TableSummaryElementTagHelperService elementTagHelperService)
    : AbpTagHelper<TableSummaryElementTagHelper, TableSummaryElementTagHelperService>(elementTagHelperService)
{
    public string Line1Text { get; set; }
    public string? Url { get; set; }

    public string? Line2Text { get; set; }
    public string ImageUrl { get; set; }
}