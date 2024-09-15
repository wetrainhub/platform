using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

#if WINDOWS
using Microsoft.UI.Xaml.Controls;
#endif

#if ANDROID
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

#if IOS || MACCATALYST
using UIKit;
#endif

namespace WTH.Platform.Maui.Handlers;
public partial class PickerVisualEffectsRemoverHandler : PickerHandler
{
    public PickerVisualEffectsRemoverHandler()
    {
    }

    public PickerVisualEffectsRemoverHandler(IPropertyMapper mapper) : base(mapper)
    {
    }
}

#if ANDROID
public partial class PickerVisualEffectsRemoverHandler : PickerHandler
{
    protected override MauiPicker CreatePlatformView()
    {
        var nativeView = base.CreatePlatformView();

        using (var gradientDrawable = new GradientDrawable())
        {
            gradientDrawable.SetColor(global::Android.Graphics.Color.Transparent);
            nativeView.SetBackground(gradientDrawable);
            nativeView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        }

        return nativeView;
    }
}
#endif

#if IOS || MACCATALYST
public partial class PickerVisualEffectsRemoverHandler : PickerHandler
{
    protected override MauiPicker CreatePlatformView()
    {
        var nativeView = base.CreatePlatformView();

        nativeView.BorderStyle = UITextBorderStyle.None;

        return nativeView;
    }
}
#endif

#if WINDOWS
public partial class PickerVisualEffectsRemoverHandler : PickerHandler
{
    protected override ComboBox CreatePlatformView()
    {
        var nativeView = base.CreatePlatformView();

        nativeView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);

        return nativeView;
    }
}
#endif