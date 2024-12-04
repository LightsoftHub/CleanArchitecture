namespace CleanArch.HttpApi.Client;

public interface ITokenProvider
{
    string? AccessToken { get; }
}
