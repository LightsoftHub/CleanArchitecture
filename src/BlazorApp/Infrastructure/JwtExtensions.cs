using BlazorApp.Core.Auth;
using Light.Identity;
using System.Text.Json;

namespace BlazorApp.Infrastructure;

public class JwtExtensions
{
    public static List<SavedClaim> ReadClaims(string jwt, IClaimType claimType)
    {
        var claims = new List<SavedClaim>();

        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs is not null)
        {
            // get roles
            var roleKey = claimType.Role;

            keyValuePairs.TryGetValue(roleKey, out object? roles);
            if (roles is not null)
            {
                string? rolesString = roles.ToString();
                if (!string.IsNullOrEmpty(rolesString))
                {
                    if (rolesString.Trim().StartsWith('['))
                    {
                        string[]? parsedRoles = JsonSerializer.Deserialize<string[]>(rolesString);

                        if (parsedRoles is not null)
                        {
                            claims.AddRange(parsedRoles.Select(role => new SavedClaim(claimType.Role, role)));
                        }
                    }
                    else
                    {
                        claims.Add(new SavedClaim(claimType.Role, rolesString));
                    }
                }
                keyValuePairs.Remove(roleKey);
            }

            // get permissions
            var permissionsKey = claimType.Permission;

            keyValuePairs.TryGetValue(permissionsKey, out object? permissions);
            if (permissions is not null)
            {
                string? permissionsString = permissions.ToString();
                if (!string.IsNullOrEmpty(permissionsString))
                {
                    if (permissionsString.Trim().StartsWith('['))
                    {
                        string[]? parsedPermissions = JsonSerializer.Deserialize<string[]>(permissionsString);

                        if (parsedPermissions is not null)
                        {
                            claims.AddRange(parsedPermissions.Select(p => new SavedClaim(permissionsKey, p)));
                        }
                    }
                    else
                    {
                        claims.Add(new SavedClaim(permissionsKey, permissionsString));
                    }
                }
                keyValuePairs.Remove(permissionsKey);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new SavedClaim(kvp.Key, kvp.Value.ToString() ?? string.Empty)));
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string payload)
    {
        payload = payload.Trim().Replace('-', '+').Replace('_', '/');
        var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        return Convert.FromBase64String(base64);
    }
}
