using System.Security.Claims;

namespace CleanArchitecture.Services;

public class ServerCurrentUser(IHttpContextAccessor httpContextAccessor) : CurrentUserBase, ICurrentUser
{
    protected override ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
}