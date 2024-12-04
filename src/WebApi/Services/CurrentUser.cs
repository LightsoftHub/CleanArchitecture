using CleanArch.Infrastructure.Auth;
using CleanArch.Shared.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArch.WebApi.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : CurrentUserBase, ICurrentUser
{
    protected override ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
}