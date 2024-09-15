using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using UraniumUI;
using Volo.Abp;
using Volo.Abp.Autofac;
using CommunityToolkit.Maui;
using WTH.Platform.Maui.Handlers;

namespace WTH.Platform.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                fonts.AddMaterialIconFonts();
            })
            .ConfigureContainer(new AbpAutofacServiceProviderFactory(new Autofac.ContainerBuilder()));

        ConfigureConfiguration(builder);

        builder.Services.AddApplication<PlatformMauiModule>(options =>
        {
            options.Services.ReplaceConfiguration(builder.Configuration);
        });

        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(Entry), typeof(EntryVisualEffectsRemoverHandler));
            handlers.AddHandler(typeof(Picker), typeof(PickerVisualEffectsRemoverHandler));
            handlers.AddHandler(typeof(DatePicker), typeof(DatePickerVisualEffectsRemoverHandler));
        });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        var app = builder.Build();

        app.Services.GetRequiredService<IAbpApplicationWithExternalServiceProvider>().Initialize(app.Services);

        return app;
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    }
}
