using System;
using WTH.Theme.Wetrainhub.TagHelpers.Table;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Enums;

namespace Wth.Crm.Web.Pages.Crm;

public class CompanyListItemViewModel
{
    [TableColumnDisplay("Company:Id")]
    [TableColumnType(TableColumnType.Checkbox)]
    [TableColumnOrder(0)]
    public Guid Id { get; set; }

    [TableColumnDisplay("Company:Name")]
    [TableColumnOrder(1)]
    [TableColumnType(TableColumnType.Summary)]
    [TableColumnTypeSummary(TableColumnSummaryType.Line1)]
    public string Name { get; set; }

    [TableColumnDisplay("Company:Email")]
    [TableColumnOrder(3)]
    [TableColumnType(TableColumnType.Email)]
    [TableColumnTypeSummary(TableColumnSummaryType.Line2)]
    public string Email { get; set; }

    [TableColumnDisplay("Company:Telephone")]
    [TableColumnOrder(2)]
    [TableColumnType(TableColumnType.Telephone)]
    public string Telephone { get; set; }

    [TableColumnIgnore] public string Address { get; set; }

    [TableColumnOrder(4)]
    [TableColumnDisplay("Company:Website")]
    [TableColumnType(TableColumnType.Url)]
    public string Website { get; set; }

    [TableColumnIgnore]
    [TableColumnTypeSummary(TableColumnSummaryType.Image)]
    public string ImageUrl => "/img/logo-header.png";

    [TableColumnTypeSummary(TableColumnSummaryType.Url)]
    [TableColumnType(TableColumnType.DetailsButton)]
    [TableColumnDisplay(true)]
    public string ProfileUrl => $"/Crm/Companies/Details/{Id}";
}