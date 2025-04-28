using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string policy) => Policy = policy;
}