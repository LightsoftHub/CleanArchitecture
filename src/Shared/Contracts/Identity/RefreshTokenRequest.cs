namespace CleanArchitecture.Contracts.Identity;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);