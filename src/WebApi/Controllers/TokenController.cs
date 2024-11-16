using CleanArch.WebApi.Models;
using Light.ActiveDirectory.Interfaces;
using Light.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/oauth")]
public class TokenController(
    IUserService userService,
    IActiveDirectoryService adService,
    ITokenService tokenService) : VersionedApiController
{
    [HttpPost("get_token")]
    public async Task<IActionResult> GetTokenAsync([FromBody] GetTokenRequest request)
    {
        var user = await userService.GetByUserNameAsync(request.UserName);
        if (user.Succeeded is false)
            return BadRequest(user);

        if (user.Data.UseDomainPassword)
        {
            var domainLogin = await adService.CheckPasswordSignInAsync(request.UserName, request.Password);

            if (domainLogin.Succeeded is false)
                return BadRequest(domainLogin);
        }
        else
        {
            var localLogin = await userService.CheckPasswordByUserNameAsync(request.UserName, request.Password);

            if (localLogin.Succeeded is false)
                return BadRequest(localLogin);
        }

        var token = await tokenService.GetTokenByUserNameAsync(request.UserName);

        return token.ToActionResult();
    }

    [HttpPost("refresh_token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        var token = await tokenService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        return token.ToActionResult();
    }
}