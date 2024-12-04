using CleanArch.Shared.Authorization;
using System.Security.Claims;

namespace CleanArch.ClientApp.Services;

public class CurrentUser(IIdentityManager identityManager) : CurrentUserBase, ICurrentUser
{
    protected override ClaimsPrincipal? User => identityManager.GetCurrentUser();
}
