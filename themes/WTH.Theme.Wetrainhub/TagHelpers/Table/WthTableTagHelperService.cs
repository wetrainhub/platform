using System;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using WTH.Theme.Wetrainhub.TagHelpers.Anchor.Email;
using WTH.Theme.Wetrainhub.TagHelpers.Anchor.External;
using WTH.Theme.Wetrainhub.TagHelpers.Anchor.Telephone;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Attributes;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.DetailsButton;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Elements.Summary;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Enums;
using WTH.Theme.Wetrainhub.TagHelpers.Table.Fields;

namespace WTH.Theme.Wetrainhub.TagHelpers.Table;

public class WthTableTagHelperService(
    IAbpTagHelperLocalizer tagHelperLocalizer,
    IModelMetadataProvider metadataProvider,
    IServiceProvider serviceProvider,
    HtmlEncoder htmlEncoder) : AbpTagHelperService<WthTableTagHelper>
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // Ensure the output tag is a table
        output.TagName = "div";
        output.Attributes.AddClass("table-responsive");

        var tableElement = CreateTableElement();

        // Retrieve the first item from the collection
        var firstItem = TagHelper.AspItems.Cast<object>().FirstOrDefault();

        if (firstItem is null)
        {
            output.SuppressOutput();
            return;
        }
        
        // Get the properties of the objects, ordered by the TableColumn attribute
        var properties = firstItem.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.GetCustomAttribute<TableColumnIgnoreAttribute>() is null)
            .OrderBy(prop => prop.GetCustomAttribute<TableColumnOrderAttribute>()?.Order ?? 999999)
            .ToArray();

        // Start building the table HTML
        await SetTableHeaderAsync(tableElement, properties, firstItem, context);

        var bodyElement = new TagBuilder("tbody");

        // Create table rows
        foreach (var item in TagHelper.AspItems)
        {
            var rowElement = new TagBuilder("tr");
            foreach (var prop in properties)
            {
                await SetTableBodyCellAsync(rowElement, prop, context, item);
            }

            bodyElement.InnerHtml.AppendHtml(rowElement);
        }

        // Set the content of the table
        tableElement.InnerHtml.AppendHtml(bodyElement);

        output.Content.SetHtmlContent(tableElement);
    }

    private async Task SetTableHeaderAsync(TagBuilder tableElement, PropertyInfo[] properties, object firstItem,
        TagHelperContext context)
    {
        var headElement = new TagBuilder("thead");
        var rowElement = new TagBuilder("tr");
        rowElement.AddCssClass("fw-semibold fs-6 text-gray-600 border-bottom-2 border-gray-200");

        var modelExplorer = metadataProvider.GetModelExplorerForType(firstItem.GetType(), firstItem);

        // Create table headers using the LocalizationKey from the attribute
        foreach (var prop in properties)
        {
            var propExplorer = modelExplorer.GetExplorerForProperty(prop.Name);
            await SetTableHeadCellAsync(rowElement, prop, context, propExplorer);
        }

        headElement.InnerHtml.SetHtmlContent(rowElement);
        tableElement.InnerHtml.AppendHtml(headElement);
    }

    private static TagBuilder CreateTableElement()
    {
        var tableElement = new TagBuilder("table");
        tableElement.AddCssClass(
            "table table-hover table-striped gy-7 gs-7 mb-0 align-middle wth-table"); // Optional: Bootstrap classes for styling
        return tableElement;
    }

    private async Task SetTableHeadCellAsync(TagBuilder rowElement, PropertyInfo prop, TagHelperContext context,
        ModelExplorer propExplorer)
    {
        var display = prop.GetCustomAttribute<TableColumnDisplayAttribute>();
        var columnType = prop.GetCustomAttribute<TableColumnTypeAttribute>()?.Type ?? TableColumnType.Text;
        var cellElement = new TagBuilder("th");

        switch (columnType)
        {
            case TableColumnType.Checkbox:
                var element = await ProcessCheckboxAsync(context, null);
                cellElement.InnerHtml.SetHtmlContent(element);
                break;
            default:
                if (!display.Hidden)
                {
                    var text = display.LocalizationKey.IsNullOrEmpty()
                        ? prop.Name
                        : tagHelperLocalizer.GetLocalizedText(display.LocalizationKey,
                            propExplorer);

                    cellElement.InnerHtml.SetHtmlContent(text);
                }

                break;
        }

        rowElement.InnerHtml.AppendHtml(cellElement);
    }

    private async Task SetTableBodyCellAsync(TagBuilder rowElement, PropertyInfo prop, TagHelperContext context,
        object item)
    {
        var value = prop.GetValue(item, null);
        var type = prop.GetCustomAttribute<TableColumnTypeAttribute>()?.Type ?? TableColumnType.Text;
        var cellElement = new TagBuilder("td");

        var element = type switch
        {
            TableColumnType.Text => value?.ToString() ?? string.Empty,
            TableColumnType.Email => await ProcessEmailLinkAsync(context, value),
            TableColumnType.Telephone => await ProcessTelephoneLinkAsync(context, value),
            TableColumnType.Url => await ProcessExternalLinkAsync(context, value),
            TableColumnType.Checkbox => await ProcessCheckboxAsync(context, value),
            TableColumnType.Summary => await ProcessSummaryAsync(context, item),
            TableColumnType.DetailsButton => await ProcessDetailsButtonAsync(context, value),
            _ => string.Empty
        };

        cellElement.InnerHtml.AppendHtml(element);

        rowElement.InnerHtml.AppendHtml(cellElement);
    }

    private async Task<string> ProcessTelephoneLinkAsync(TagHelperContext context, object? item)
    {
        var tagHelper = serviceProvider.GetRequiredService<AnchorTelephoneLinkTagHelper>();
        tagHelper.ViewContext = TagHelper.ViewContext;

        var telephone = item?.ToString() ?? string.Empty;
        tagHelper.Text = telephone;

        var attributes = new TagHelperAttributeList
        {
            new TagHelperAttribute("href", telephone)
        };

        var element =
            await tagHelper.RenderAsync(attributes, context, htmlEncoder, "a", TagMode.StartTagAndEndTag);
        return element;
    }

    private async Task<string> ProcessEmailLinkAsync(TagHelperContext context, object? item)
    {
        var tagHelper = serviceProvider.GetRequiredService<AnchorEmailLinkTagHelper>();
        tagHelper.ViewContext = TagHelper.ViewContext;

        var email = item?.ToString() ?? string.Empty;
        tagHelper.Text = email;

        var attributes = new TagHelperAttributeList
        {
            new TagHelperAttribute("href", email)
        };

        var element =
            await tagHelper.RenderAsync(attributes, context, htmlEncoder, "a", TagMode.StartTagAndEndTag);
        return element;
    }

    private async Task<string> ProcessExternalLinkAsync(TagHelperContext context, object? item)
    {
        var abpInputTagHelper = serviceProvider.GetRequiredService<AnchorExternalLinkTagHelper>();
        abpInputTagHelper.ViewContext = TagHelper.ViewContext;

        var url = item?.ToString() ?? string.Empty;
        abpInputTagHelper.Text = url;

        var attributes = new TagHelperAttributeList
        {
            new TagHelperAttribute("href", url)
        };

        var element =
            await abpInputTagHelper.RenderAsync(attributes, context, htmlEncoder, "a", TagMode.StartTagAndEndTag);
        return element;
    }

    private async Task<string> ProcessDetailsButtonAsync(TagHelperContext context, object? item)
    {
        var abpInputTagHelper = serviceProvider.GetRequiredService<TableDetailsButtonElementTagHelper>();
        abpInputTagHelper.ViewContext = TagHelper.ViewContext;
        abpInputTagHelper.Url = item?.ToString() ?? string.Empty;

        var element = await abpInputTagHelper.RenderAsync([], context, htmlEncoder, "div", TagMode.StartTagAndEndTag);
        return element;
    }

    private async Task<string> ProcessSummaryAsync(TagHelperContext context, object row)
    {
        var properties = row.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.GetCustomAttribute<TableColumnTypeSummaryAttribute>() is not null)
            .ToArray();

        var line1 = GetSummaryPropertyValue(row, properties, TableColumnSummaryType.Line1);
        var line2 = GetSummaryPropertyValue(row, properties, TableColumnSummaryType.Line2);
        var imageUrl = GetSummaryPropertyValue(row, properties, TableColumnSummaryType.Image);
        var url = GetSummaryPropertyValue(row, properties, TableColumnSummaryType.Url);

        var abpInputTagHelper = serviceProvider.GetRequiredService<TableSummaryElementTagHelper>();
        abpInputTagHelper.Line1Text = line1;
        abpInputTagHelper.Line2Text = line2;
        abpInputTagHelper.Url = url;
        abpInputTagHelper.ImageUrl = imageUrl;
        abpInputTagHelper.ViewContext = TagHelper.ViewContext;

        var element = await abpInputTagHelper.RenderAsync([], context, htmlEncoder, "div", TagMode.StartTagAndEndTag);
        return element;
    }

    private static string? GetSummaryPropertyValue(object row, PropertyInfo[] properties, TableColumnSummaryType type)
    {
        var line1Property = properties.FirstOrDefault(f =>
                f.GetCustomAttribute<TableColumnTypeSummaryAttribute>()?.Type == type)
            ?.GetValue(row, null)
            ?.ToString();
        return line1Property;
    }

    protected virtual async Task<string> ProcessCheckboxAsync(TagHelperContext context, object? value)
    {
        var abpInputTagHelper = serviceProvider.GetRequiredService<TableCheckboxElementTagHelper>();
        abpInputTagHelper.Checked = value is true;
        abpInputTagHelper.Value = value;
        abpInputTagHelper.ViewContext = TagHelper.ViewContext;

        var element = await abpInputTagHelper.RenderAsync([], context, htmlEncoder, "div", TagMode.StartTagAndEndTag);
        return element;
    }
}