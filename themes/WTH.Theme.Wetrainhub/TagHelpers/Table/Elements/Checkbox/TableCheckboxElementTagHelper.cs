using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Fields;

public class TableCheckboxElementTagHelper(TableCheckboxElementTagHelperService elementTagHelperService)
    : AbpTagHelper<TableCheckboxElementTagHelper, TableCheckboxElementTagHelperService>(elementTagHelperService)
{
    public bool Checked { get; set; }
    public object? Value { get; set; }
}