using Microsoft.AspNetCore.Authorization;

namespace CleanArch.WebApi.Infrastructure.Auth;

public static class Startup
{
    public static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

}