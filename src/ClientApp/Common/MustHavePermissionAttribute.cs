using Microsoft.AspNetCore.Authorization;

namespace CleanArch.ClientApp.Common;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string policy) => Policy = policy;
}