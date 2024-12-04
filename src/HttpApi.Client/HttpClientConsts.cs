using System.Text.Json;

namespace CleanArch.HttpApi.Client;

public class HttpClientConsts
{
    public const string BackendApi = nameof(BackendApi);

    public const string RetailApi = nameof(RetailApi);

    public static JsonSerializerOptions JsonSerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true,
    };
}
