namespace BlazorApp.Core;

public interface IStorageService
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask SetAsync<T>(string key, T data);

    ValueTask RemoveAsync(string key);

    ValueTask ClearAsync();
}
