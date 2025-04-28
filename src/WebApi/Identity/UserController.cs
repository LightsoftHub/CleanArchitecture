using Light.ActiveDirectory.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Identity;

[MustHavePermission(Permissions.Users.View)]
public class UserController(
    IUserService userService,
    IActiveDirectoryService activeDirectoryService,
    IUserAttributeService userAttributeService) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        return Ok(await userService.GetByIdAsync(id));
    }

    [HttpGet("by_username/{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        return Ok(await userService.GetByUserNameAsync(username));
    }

    [HttpPost]
    [MustHavePermission(Permissions.Users.Create)]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserRequest request)
    {
        var res = await userService.CreateAsync(request);
        return Ok(res);
    }

    [HttpPut("{id}")]
    [MustHavePermission(Permissions.Users.Update)]
    public async Task<IActionResult> PutAsync(string id, UserDto request)
    {
        if (id != request.Id)
        {
            return Ok(Result.Error("Validate User ID not match"));
        }

        return Ok(await userService.UpdateAsync(request));
    }

    [HttpDelete("{id}")]
    [MustHavePermission(Permissions.Users.Delete)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        return Ok(await userService.DeleteAsync(id));
    }

    [HttpPut("{id}/password/force")]
    [MustHavePermission(Permissions.Users.Update)]
    public async Task<IActionResult> ForcePasswordAsync(string id, [FromBody] string password)
    {
        return Ok(await userService.ForcePasswordAsync(id, password));
    }

    [HttpGet("get_domain_user/{userName}")]
    public async Task<IActionResult> GetDomainUserAsync([FromRoute] string userName)
    {
        return Ok(await activeDirectoryService.GetByUserNameAsync(userName));
    }

    [HttpGet("{userId}/attributes")]
    public async Task<IActionResult> GetAttributes([FromRoute] string userId)
    {
        var res = await userAttributeService.GetByAsync(userId);
        return Ok(res);
    }

    [HttpPost("{userId}/attribute/{key}")]
    public async Task<IActionResult> AddAttribute([FromRoute] string userId, [FromRoute] string key, [FromBody] string value)
    {
        await userAttributeService.AddAsync(userId, key, value);
        return Ok();
    }

    [HttpDelete("{userId}/attribute/{key}")]
    public async Task<IActionResult> DeleteAttribute([FromRoute] string userId, [FromRoute] string key)
    {
        await userAttributeService.DeleteAsync(userId, key);
        return Ok();
    }

    [HttpGet("attributes")]
    public async Task<IActionResult> SearchAttributes(string key, string value)
    {
        var res = await userAttributeService.GetUsersAsync(key, value);
        return Ok(res);
    }
}
