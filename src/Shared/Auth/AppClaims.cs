using System.Reflection;
using System.Security.Claims;

namespace CleanArchitechture.Auth;

public abstract class AppClaims
{
    public const string UserId = "uid";

    public const string UserName = "un";

    public const string FirstName = "first_name";

    public const string LastName = "last_name";

    public const string FullName = "full_name";

    public const string PhoneNumber = "phone_number";

    public const string Email = "email";

    public const string Role = "role";

    public const string Permission = "permission";

    public const string ImageUrl = "image_url";

    public const string Expiration = "exp";

    public const string AccessToken = "token";

    public const string TenantId = "tenant_id";

    public static IEnumerable<Claim> All
    {
        get
        {
            var fromClass = typeof(Permissions);

            var claims = new List<Claim>();

            // get classes in class
            var modules = fromClass.GetNestedTypes();

            foreach (var module in modules)
            {
                // get props in class
                var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (FieldInfo fi in fields)
                {
                    var propertyValue = fi.GetValue(null);

                    if (propertyValue != null)
                    {
                        claims.Add(new Claim(Permission, propertyValue.ToString() ?? string.Empty));
                    }
                    //TODO - take descriptions from description attribute
                }
            }

            return claims;
        }
    }

    public static bool IsPermissionValid(string permission) =>
        All.Any(x => x.Type == Permission && x.Value == permission);
}