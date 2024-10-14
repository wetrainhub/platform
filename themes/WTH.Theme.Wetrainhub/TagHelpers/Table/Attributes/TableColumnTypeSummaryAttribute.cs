using System;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColumnTypeSummaryAttribute(TableColumnSummaryType type) : Attribute
    {
        public TableColumnSummaryType Type { get; } = type;
    }

    public enum TableColumnSummaryType
    {
        Line1,
        Line2,
        Url,
        Image
    }
}