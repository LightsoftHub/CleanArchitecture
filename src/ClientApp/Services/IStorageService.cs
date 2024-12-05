using Blazored.LocalStorage;

namespace CleanArch.ClientApp.Services;

public interface IStorageService
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask SetAsync<T>(string key, T data);

    ValueTask RemoveAsync(string key);

    ValueTask ClearAsync();
}
