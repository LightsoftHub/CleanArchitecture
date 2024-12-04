namespace CleanArch.Shared.Notifications;

public class NotificationLookup : PageLookup
{
    public string? ToUser { get; set; }

    public bool OnlyUnread { get; set; } = false;
}