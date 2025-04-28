using Light.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Identity;

/// <summary>
/// move to other controller for current_user can get data when login without permission
/// </summary>
public class UserProfileController(
    ICurrentUser currentUser,
    IUserAttributeService userAttributeService,
    ITokenService tokenService) : ApiControllerBase
{
    private readonly string _userId = currentUser.UserId ?? throw new UnauthorizedException();

    [HttpGet("attributes")]
    public async Task<IActionResult> GetAttributes()
    {
        var res = await userAttributeService.GetByAsync(_userId);
        return Ok(res);
    }

    [HttpGet("token/list")]
    public async Task<IActionResult> GetTokens()
    {
        var res = await tokenService.GetUserTokensAsync(_userId);
        return Ok(res);
    }

    [HttpPut("token/revoke")]
    public async Task<IActionResult> RevokeToken([FromBody] string tokenId)
    {
        await tokenService.RevokedAsync(_userId, tokenId);
        return Ok();
    }
}
