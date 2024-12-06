using Microsoft.AspNetCore.Authorization;

namespace CleanArch.ClientApp.Common;

internal class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}