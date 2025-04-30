namespace CleanArchitecture;

public interface ITokenProvider
{
    //Task<string?> AccessToken { get; }

    Task<string?> GetAccessTokenAsync();
}
