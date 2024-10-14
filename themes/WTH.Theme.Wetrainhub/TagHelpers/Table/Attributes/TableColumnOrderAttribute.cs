using System;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColumnOrderAttribute(int order) : Attribute
    {
        public int Order { get; } = order;
    }
}