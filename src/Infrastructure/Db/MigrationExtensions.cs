using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitechture.Db;

public static class MigrationExtensions
{
    public static IServiceCollection AddMigratorServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUser, MigrationUser>();

        return services;
    }

    public static async Task SeedDatabase<TContext>(this TContext context, ILogger logger)
        where TContext : DbContext
    {
        var dbName = context.Database.GetDbConnection().Database;

        logger.LogInformation("database {name} initializing ...", dbName);

        try
        {
            if (context.Database.GetMigrations().Any())
            {
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();

                    logger.LogInformation("database {name} initialized", dbName);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}

internal class MigrationUser : ICurrentUser
{
    public string? UserId => "Migrator";

    public string? Username => throw new NotImplementedException();

    public string? FirstName => throw new NotImplementedException();

    public string? LastName => throw new NotImplementedException();

    public string? FullName => throw new NotImplementedException();

    public string? PhoneNumber => throw new NotImplementedException();

    public string? Email => throw new NotImplementedException();

    public bool IsAuthenticated => throw new NotImplementedException();

    public bool IsMasterUser => throw new NotImplementedException();

    public string? EmployeeId => throw new NotImplementedException();

    public bool HasPermission(string permission)
    {
        throw new NotImplementedException();
    }

    public bool IsInRole(string role)
    {
        throw new NotImplementedException();
    }
}
