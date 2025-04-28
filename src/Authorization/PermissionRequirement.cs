using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}