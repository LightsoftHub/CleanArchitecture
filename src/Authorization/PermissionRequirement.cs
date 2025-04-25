using Microsoft.AspNetCore.Authorization;

namespace CleanArchitechture;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; private set; } = permission;
}