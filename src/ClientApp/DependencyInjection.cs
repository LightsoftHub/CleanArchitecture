using CleanArch.ClientApp.Common;
using CleanArch.ClientApp.Infrastructure;
using CleanArch.ClientApp.Infrastructure.Identity;
using CleanArch.ClientApp.Pages.Roles;
using CleanArch.ClientApp.Services;
using CleanArch.Shared.Authorization;
using Light.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArch.ClientApp;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IToastDisplay, ToastDisplay>();
        services.AddScoped<SpinnerService>();
        services.AddScoped<ICallGuardedService, CallGuardedService>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthorizationCore(RegisterPermissions);
        services.AddCascadingAuthenticationState();
        // register the custom state provider
        services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        services.AddScoped(sp => (IIdentityManager)sp.GetRequiredService<AuthenticationStateProvider>());
        services.AddScoped<ICurrentUser, ClientCurrentUser>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }

    private static void RegisterPermissions(AuthorizationOptions options)
    {
        var permissions = ClientClaimsExtensions.GetAll().Claims.Select(s => s.Value).ToList();

        foreach (var permission in permissions)
        {
            options.AddPolicy(permission, policy =>
                policy.AddRequirements(new PermissionRequirement(permission)));
        }
    }
}
