namespace WTH.Platform.Maui.Extensions;

public abstract class MarkupExtensionBase
{
    protected IServiceProvider RootServiceProvider => 
#if WINDOWS
            MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
         throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
}