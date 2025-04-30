using CleanArchitecture.Contracts.Notifications;

namespace CleanArchitecture.Notifications;

public interface INotificationService
{
    Task<PagedResult<NotificationDto>> GetAsync(NotificationLookup request);

    Task<NotificationDto?> GetByIdAsync(string id);

    Task<int> CountUnreadAsync(string userId);

    Task SaveAsync(string fromUserId, string? fromName, string toUserId, SystemMessage message);

    Task ReadAsync(string id);
}
