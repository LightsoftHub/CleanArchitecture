using Blazored.LocalStorage;
using CleanArch.ClientApp;
using CleanArch.ClientApp.Infrastructure;
using CleanArch.ClientApp.Infrastructure.Identity;
using CleanArch.ClientApp.Services;
using CleanArch.HttpApi.Client;
using CleanArch.Shared.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<IToastDisplay, ToastDisplay>();
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddScoped<ICallGuardedService, CallGuardedService>();

builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddHttpServices(typeof(HttpApiClientModule).Assembly);
builder.Services.AddScoped<ITokenProvider, JwtTokenProvider>();

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<IStorageService, StorageService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
// register the custom state provider
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IIdentityManager)sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddScoped<ICurrentUser, ClientCurrentUser>();

await builder.Build().RunAsync();
