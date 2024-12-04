using Microsoft.AspNetCore.Authorization;

namespace CleanArch.WebApi.Infrastructure.Auth.Permissions;

internal class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}