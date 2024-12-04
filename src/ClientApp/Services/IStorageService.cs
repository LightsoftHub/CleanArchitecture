using Blazored.LocalStorage;

namespace CleanArch.ClientApp.Services;

public interface IStorageService
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask SetAsync<T>(string key, T data);

    ValueTask RemoveAsync(string key);

    ValueTask ClearAsync();
}

public class StorageService(ILocalStorageService localStorage) : IStorageService
{
    public ValueTask<T?> GetAsync<T>(string key) => localStorage.GetItemAsync<T>(key);

    public ValueTask SetAsync<T>(string key, T data) => localStorage.SetItemAsync<T>(key, data);

    public ValueTask RemoveAsync(string key) => localStorage.RemoveItemAsync(key);

    public ValueTask ClearAsync() => localStorage.ClearAsync();
}
