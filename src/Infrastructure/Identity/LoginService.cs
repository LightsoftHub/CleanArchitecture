using Light.ActiveDirectory.Interfaces;
using Light.Identity;
using Light.Identity.EntityFrameworkCore;
using Light.Identity.Models;
using Light.Identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CleanArchitechture.Identity;

internal class LoginService(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IClaimType claimType,
    IOptions<JwtOptions> jwtOptions,
    IIdentityContext context,
    IActiveDirectoryService domainService) :
    TokenService(userManager, roleManager, claimType, jwtOptions, context),
    ILoginService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<IResult<TokenDto>> GetTokenAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        var errorResult = Result<TokenDto>.Error("Invalid credentials");

        if (user == null || user.Status.IsActive is false)
            return errorResult;

        bool isPasswordValid;

        if (user.UseDomainPassword)
        {
            isPasswordValid = await domainService.CheckPasswordSignInAsync(username, password);
        }
        else
        {
            var checkLocalPassword = await _userManager.CheckPasswordAsync(user, password);
            isPasswordValid = checkLocalPassword;
        }

        if (isPasswordValid is false)
        {
            return errorResult;
        }

        return await GetTokenAsync(user);
    }
}
