using Microsoft.Extensions.DependencyInjection;
using Migrator.SqlServer;

// set Environment
//Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Live");

using var host = Host.CreateHostBuilder(args).Build();

using var scope = host.Services.CreateScope();

var serviceProvider = scope.ServiceProvider;



var initialiser = serviceProvider.GetRequiredService<IdentityContextInitialiser>();

await initialiser.InitialiseAsync();

await initialiser.TrySeedAsync();