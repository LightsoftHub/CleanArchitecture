using Microsoft.AspNetCore.Authorization;

namespace CleanArch.WebApi.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string policy) => Policy = policy;
}