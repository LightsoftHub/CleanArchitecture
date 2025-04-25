namespace CleanArchitechture.Contracts.Identity;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);