using WTH.Platform.Maui.Localization;

namespace WTH.Platform.Maui.Extensions;

[ContentProperty(nameof(Text))]
public class TranslateExtension : MarkupExtensionBase, IMarkupExtension<BindingBase>
{
    public string Text { get; set; } = string.Empty;

    public string? StringFormat { get; set; }

    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        var localizationResourceManager = RootServiceProvider.GetRequiredService<LocalizationResourceManager>();
        
        var binding = new Binding
        {
            Mode = BindingMode.OneWay,
            Path = $"[{Text}]",
            Source = localizationResourceManager,
            StringFormat = StringFormat
        };
        return binding;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
}
