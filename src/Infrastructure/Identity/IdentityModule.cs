using CleanArchitechture.Db;
using CleanArchitechture.Modularity;
using Light.ActiveDirectory;
using Light.Extensions.DependencyInjection;
using Light.Identity;
using Light.Identity.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitechture.Identity;

public class IdentityModule : AppModule
{
    public override void Add(IServiceCollection services, IConfiguration configuration)
    {
        if (AppConfiguration.IsUseInMemoryDatabase)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityDb"));
        }
        else
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(DbConnectionNames.DEFAULT)));
        }

        services.AddIdentity<AppIdentityDbContext>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;

            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // Lockout settings
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
            //options.Lockout.MaxFailedAccessAttempts = 10;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        AddAuth(services, configuration);

        services.AddScoped<ILoginService, LoginService>();
    }

    private void AddAuth(IServiceCollection services, IConfiguration configuration)
    {
        var sectionName = "JWT";

        // Override by BindConfiguration
        services.AddOptions<JwtOptions>().BindConfiguration(sectionName);
        services.AddJwtTokenProvider<AppClaimTypes>();

        // add JWT Auth
        var jwtSettings = configuration.GetSection(sectionName).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtSettings, nameof(JwtOptions));
        services.AddJwtAuth(jwtSettings.Issuer, jwtSettings.SecretKey); // inject this for use jwt auth

        // connect to AD
        var domainName = configuration.GetValue<string>("MemberOfDomain");
        if (!string.IsNullOrEmpty(domainName))
        {
            services.AddActiveDirectory(x => x.Name = domainName);
        }
        else
        {
            services.AddActiveDirectory();
        }
    }
}