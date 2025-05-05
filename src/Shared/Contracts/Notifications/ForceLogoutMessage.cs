namespace CleanArchitecture.Contracts.Notifications;

public record ForceLogoutMessage(string UserId) : INotificationMessage;
