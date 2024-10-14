using System;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Enums;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColumnTypeAttribute(TableColumnType type) : Attribute
    {
        public TableColumnType Type { get; } = type;
    }
}