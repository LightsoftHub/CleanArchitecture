using CleanArchitecture.Contracts.Notifications;
using CleanArchitecture.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.SignalR;

internal class NotificationHubContext(IHubContext<SignalRHub> hubContext) : IHubSender
{
    public Task SendAllAsync(INotificationMessage data, CancellationToken cancellationToken = default)
    {
        return hubContext.Clients.All
            .SendAsync(data.GetType().Name, data, cancellationToken);
    }

    public Task SendAsync(string userId, INotificationMessage data, CancellationToken cancellationToken = default)
    {
        return hubContext.Clients.User(userId)
            .SendAsync(data.GetType().Name, data, cancellationToken);
    }

    public Task SendAsync(IReadOnlyList<string> userIds, INotificationMessage data, CancellationToken cancellationToken = default)
    {
        return hubContext.Clients.Users(userIds)
            .SendAsync(data.GetType().Name, data, cancellationToken);
    }
}
