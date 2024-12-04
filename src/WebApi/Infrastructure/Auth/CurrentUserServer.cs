using CleanArch.Shared.Authorization;
using System.Security.Claims;

namespace CleanArch.WebApi.Infrastructure.Auth;

public class CurrentUserServer(IHttpContextAccessor httpContextAccessor) : CurrentUserBase, ICurrentUser
{
    protected override ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
}