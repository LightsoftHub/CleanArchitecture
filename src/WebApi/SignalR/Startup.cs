using CleanArch.WebApi.SignalR.Services;
using Microsoft.AspNetCore.SignalR;

namespace CleanArch.WebApi.SignalR;

public static class Startup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSignalR();

        /* use only for Services API */
        services.AddSingleton<IUserIdProvider, CustomIdProvider>();

        services.AddScoped<NotificationHub>();

        services.AddScoped<INotifyService, NotifyService>();

        return services;
    }

    public static IEndpointRouteBuilder MapNotificationHub(this IEndpointRouteBuilder endpoints, string? endpoint = null)
    {
        endpoints.MapHub<NotificationHub>(endpoint ?? "/signalr-hub", options =>
        {
            options.CloseOnAuthenticationExpiration = true;
        });

        return endpoints;
    }
}