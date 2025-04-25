using CleanArchitechture.Auth;
using Light.Identity;

namespace CleanArchitechture.Identity;

public class AppClaimTypes : IClaimType
{
    public string AccessToken => AppClaims.AccessToken;

    public string Email => AppClaims.Email;

    public string Expiration => AppClaims.Expiration;

    public string FirstName => AppClaims.FirstName;

    public string FullName => AppClaims.FullName;

    public string ImageUrl => AppClaims.ImageUrl;

    public string LastName => AppClaims.LastName;

    public string Permission => AppClaims.Permission;

    public string PhoneNumber => AppClaims.PhoneNumber;

    public string Role => AppClaims.Role;

    public string UserId => AppClaims.UserId;

    public string UserName => AppClaims.UserName;
}
