using System;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColumnDisplayAttribute
        : Attribute
    {
        public TableColumnDisplayAttribute(bool hidden)
        {
            Hidden = hidden;
            LocalizationKey = string.Empty;
        }

        public TableColumnDisplayAttribute(string localizationKey)
        {
            LocalizationKey = localizationKey;
        }
        
        public string LocalizationKey { get; }
        public bool Hidden { get; }
    }
}