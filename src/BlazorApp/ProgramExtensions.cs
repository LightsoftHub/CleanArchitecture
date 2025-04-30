global using CleanArchitecture;
global using CleanArchitecture.Auth;
using BlazorApp.Core;
using BlazorApp.Core.Auth;
using BlazorApp.Infrastructure;
using Blazored.LocalStorage;
using Light.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

namespace BlazorApp;

public static class ProgramExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFileGenerator();

        services.AddBlazoredLocalStorageAsSingleton();
        services.AddSingleton<IStorageService, StorageService>();

        services.AddSingleton<IWebSettings, WebSettings>();
        services.AddScoped<IToastDisplay, ToastDisplay>();
        services.AddScoped<SpinnerService>();
        services.AddScoped<ICallGuardedService, CallGuardedService>();
        services.AddScoped<SignalRClient>();
        services.AddScoped<LayoutService>();

        services.AddScoped<IAppTokenService, TokenService>();

        services.AddHttpClients(configuration);
        services.AddHttpServices(typeof(HttpApiClientModule).Assembly);

        services.AddMudServices();

        services.AddAuth();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthorizationCore(RegisterPermissions);
        services.AddCascadingAuthenticationState();
        // register the custom state provider
        services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        services.AddScoped(sp => (IIdentityManager)sp.GetRequiredService<AuthenticationStateProvider>());
        services.AddScoped(sp => (ITokenProvider)sp.GetRequiredService<AuthenticationStateProvider>());
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