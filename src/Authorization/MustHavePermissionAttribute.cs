using Microsoft.AspNetCore.Authorization;

namespace CleanArchitechture;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string policy) => Policy = policy;
}