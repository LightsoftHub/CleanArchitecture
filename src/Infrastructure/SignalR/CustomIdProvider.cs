using CleanArchitecture.Auth;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.SignalR;

public class CustomIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var userId = connection.User?.FindFirst(AppClaims.UserId)?.Value;
        return userId;
    }
}