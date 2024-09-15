using Volo.Abp.DependencyInjection;

namespace WTH.Platform.Maui.Storage;

public class DefaultStorage : IStorage, ITransientDependency
{
    public async Task<string> GetAsync(string key)
    {
#if DEBUG
        return Preferences.Get(key, string.Empty);
#else
        return await SecureStorage.Default.GetAsync(key);
#endif
    }

    public async Task SetAsync(string key, string value)
    {
        if (value.IsNullOrEmpty())
        {
            await RemoveAsync(key);
            return;
        }
#if DEBUG
        Preferences.Set(key, value);
#else
        await SecureStorage.Default.SetAsync(key, value);
#endif
    }

    public Task RemoveAsync(string key)
    {
#if DEBUG
        Preferences.Remove(key);
#else
        SecureStorage.Default.Remove(key);
#endif
        return Task.CompletedTask;
    }
}