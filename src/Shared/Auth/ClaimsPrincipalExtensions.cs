using System.Security.Claims;

namespace CleanArchitecture.Auth;

public static class ClaimsPrincipalExtensions
{
    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;

    public static string? GetUserId(this ClaimsPrincipal principal) =>
        principal?.FindFirstValue(AppClaims.UserId);

    public static string? GetUserName(this ClaimsPrincipal principal) =>
        principal?.FindFirstValue(AppClaims.UserName);

    public static string? GetFullName(this ClaimsPrincipal principal) =>
        principal?.FindFirstValue(AppClaims.FullName);

    public static string? GetFirstName(this ClaimsPrincipal principal) =>
        principal?.FindFirstValue(AppClaims.FirstName);

    public static string? GetLastName(this ClaimsPrincipal principal) =>
        principal?.FindFirstValue(AppClaims.LastName);

    public static string? GetEmail(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(AppClaims.Email);

    public static string? GetPhoneNumber(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(AppClaims.PhoneNumber);

    public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
        DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
            principal.FindFirstValue(AppClaims.Expiration)));

    public static bool IsAuthenticated(this ClaimsPrincipal principal) =>
        principal.Identity?.IsAuthenticated is true;

    public static bool HasPermission(this ClaimsPrincipal principal, string claimType, string claimValue) =>
        principal.HasClaim(claimType, claimValue) is true;
}