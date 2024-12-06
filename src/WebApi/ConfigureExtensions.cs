using CleanArch.Application;
using CleanArch.Infrastructure;
using CleanArch.Shared.Authorization;
using CleanArch.WebApi.Infrastructure.Auth;
using Light.ActiveDirectory;
using Light.AspNetCore.Builder;
using Light.AspNetCore.CORS;
using Light.AspNetCore.Swagger;
using Light.Extensions.DependencyInjection;
using Light.Identity;
using Light.Identity.Options;

namespace CleanArch.WebApi;

public static class ConfigureExtensions
{
    private const bool swaggerVersionDefinition = true;

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersion(1);
        services.AddSwagger(configuration);

        services.AddAuth(configuration);

        services.AddCorsPolicies(configuration);

        services
            .AddInfrastructure(configuration)
            .AddApplication();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, ServerCurrentUser>();

        services.AddPermissions();

        var sectionName = "JWT";

        // Overide by BindConfiguration
        services.AddOptions<JwtOptions>().BindConfiguration(sectionName);

        services.AddTokenServices();
        services.AddActiveDirectory();

        // add JWT Auth
        var jwtSettings = configuration.GetSection(sectionName).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtSettings, nameof(JwtOptions));
        services.AddJwtAuth(jwtSettings.Issuer, jwtSettings.SecretKey); // inject this for use jwt auth

        var domainName = configuration.GetValue<string>("AD");
        if (domainName != null)
        {
            services.AddActiveDirectory(x => x.Name = domainName);
        }

        return services;
    }

    public static IApplicationBuilder ConfigurePipelines(this IApplicationBuilder builder, IConfiguration configuration) =>
    builder
        .UseUlidTraceId()
        .UseLightRequestLogging()
        .UseLightExceptionHandler()
        .UseRouting()
        .UseCorsPolicies(configuration) // must add before Auth
        .UseAuthentication()
        .UseAuthorization()
        .UseSwagger();

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder, bool allowAnonymous = false)
    {
        if (allowAnonymous)
        {
            builder.MapControllers().AllowAnonymous();
        }
        else
        {
            builder.MapControllers().RequireAuthorization();
        }

        return builder;
    }
}