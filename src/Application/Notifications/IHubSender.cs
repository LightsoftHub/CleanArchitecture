using CleanArchitecture.Contracts.Notifications;

namespace CleanArchitecture.Notifications;

public interface IHubSender
{
    Task SendAllAsync(INotificationMessage data, CancellationToken cancellationToken = default);

    Task SendAsync(string userId, INotificationMessage data, CancellationToken cancellationToken = default);

    Task SendAsync(IReadOnlyList<string> userIds, INotificationMessage data, CancellationToken cancellationToken = default);
}
