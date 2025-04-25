using CleanArchitechture.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Identity;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/oauth")]
public class TokenController(
    ILoginService loginService,
    ITokenService tokenService) : ApiControllerBase
{
    [HttpPost("token/get")]
    public async Task<IActionResult> GetToken([FromBody] GetTokenRequest request)
    {
        var res = await loginService.GetTokenAsync(request.Username, request.Password);
        return Ok(res);
    }

    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var res = await tokenService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
        return Ok(res);
    }
}