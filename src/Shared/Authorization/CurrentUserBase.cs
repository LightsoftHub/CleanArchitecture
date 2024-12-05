﻿using System.Security.Claims;

namespace CleanArch.Shared.Authorization;

public abstract class CurrentUserBase : ICurrentUser
{
    protected virtual ClaimsPrincipal? User { get; set; }

    public string? UserId => User?.GetUserId();

    public string? Username => User?.GetUserName();

    public string? FirstName => User?.GetFirstName();

    public string? LastName => User?.GetLastName();

    public string? FullName => $"{FirstName} {LastName}";

    public string? PhoneNumber => User?.GetPhoneNumber();

    public string? Email => User?.GetEmail();

    public bool IsAuthenticated => User?.IsAuthenticated() is true;

    public bool IsMasterUser => DefaultUser.MASTER_USERS.Any(x => x == Username);

    public bool IsInRole(string role) => User?.IsInRole(role) is true;

    public bool HasPermission(string permission) =>
        User?.HasPermission(Light.Identity.ClaimTypes.Permission, permission) is true
        || IsMasterUser;
}