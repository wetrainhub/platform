namespace WTH.Platform.Maui.Storage;

public interface IStorage
{
    Task<string> GetAsync(string key);

    Task SetAsync(string key, string value);

    Task RemoveAsync(string key);
}