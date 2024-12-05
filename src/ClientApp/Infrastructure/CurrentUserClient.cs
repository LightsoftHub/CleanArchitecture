using CleanArch.ClientApp.Services;
using CleanArch.Shared.Authorization;
using System.Security.Claims;

namespace CleanArch.ClientApp.Infrastructure;

public class CurrentUserClient(IIdentityManager identityManager) : CurrentUserBase, ICurrentUser
{
    protected override ClaimsPrincipal? User => identityManager.GetCurrentUser();
}
