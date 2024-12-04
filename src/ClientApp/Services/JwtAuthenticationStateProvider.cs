using CleanArch.HttpApi.Client.Identity;
using Light.Contracts;
using Light.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CleanArch.ClientApp.Services;

public class JwtAuthenticationStateProvider(
    IStorageService storageService,
    TokenHttpService tokenService) : AuthenticationStateProvider, IIdentityManager
{
    private const string jwtCacheKey = "jwt_token";

    public ClaimsPrincipal User = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await storageService.GetAsync<TokenDto>(jwtCacheKey);

        if (jwtToken is not null)
        {
            var userClaims = JwtExtensions.ReadClaims(jwtToken.AccessToken);

            // must set authenticationType to mark isAuthentitcated = true
            var identity = new ClaimsIdentity(userClaims, "JWT");

            User = new ClaimsPrincipal(identity);
        }

        return new AuthenticationState(User);
    }

    public ClaimsPrincipal GetCurrentUser() => User;

    public async Task<Result> LoginAsync(string userName, string password)
    {
        var getToken = await tokenService.GetTokenAsync(userName, password);

        if (getToken.Succeeded is false)
            return Result.Error(getToken.Message);

        await storageService.SetAsync(jwtCacheKey, getToken.Data);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        return Result.Success();
    }

    public async Task LogoutAsync()
    {
        await storageService.ClearAsync();
        
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymous));

        NotifyAuthenticationStateChanged(authState);
    }
}
