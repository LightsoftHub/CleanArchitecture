using Light.Contracts;
using System.Security.Claims;

namespace CleanArch.ClientApp.Services;

public interface IIdentityManager
{
    ClaimsPrincipal GetCurrentUser();

    Task<Result> LoginAsync(string userName, string password);

    Task LogoutAsync();
}
